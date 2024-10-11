import socket
import json
import cv2
import numpy as np
from scipy.ndimage import zoom
from imutils import face_utils
import dlib
from tensorflow.keras.models import load_model

def show_webcam():
    shape_x, shape_y = 48, 48
    model = load_model('Models/video.h5')
    face_detect = dlib.get_frontal_face_detector()
    predictor_landmarks = dlib.shape_predictor("Models/face_landmarks.dat")
    
    video_capture = cv2.VideoCapture(0)

    # Fungsi untuk menambahkan bayangan pada teks
    def draw_text_with_shadow(img, text, x, y, font, scale, color, thickness):
        cv2.putText(img, text, (x + 2, y + 2), font, scale, (0, 0, 0), thickness + 2, cv2.LINE_AA)
        cv2.putText(img, text, (x, y), font, scale, color, thickness, cv2.LINE_AA)

    # Fungsi untuk membuat kotak deteksi dengan sudut tumpul
    def draw_rounded_rectangle(img, top_left, bottom_right, color, thickness, corner_radius=15):
        x1, y1 = top_left
        x2, y2 = bottom_right
        if thickness < 0:  # Filled rectangle
            cv2.rectangle(img, (x1, y1), (x2, y2), color, cv2.FILLED)
        else:
            # Draw the 4 corners
            cv2.line(img, (x1 + corner_radius, y1), (x2 - corner_radius, y1), color, thickness)
            cv2.line(img, (x1 + corner_radius, y2), (x2 - corner_radius, y2), color, thickness)
            cv2.line(img, (x1, y1 + corner_radius), (x1, y2 - corner_radius), color, thickness)
            cv2.line(img, (x2, y1 + corner_radius), (x2, y2 - corner_radius), color, thickness)
            # Draw rounded corners
            cv2.ellipse(img, (x1 + corner_radius, y1 + corner_radius), (corner_radius, corner_radius), 180, 0, 90, color, thickness)
            cv2.ellipse(img, (x2 - corner_radius, y1 + corner_radius), (corner_radius, corner_radius), 270, 0, 90, color, thickness)
            cv2.ellipse(img, (x1 + corner_radius, y2 - corner_radius), (corner_radius, corner_radius), 90, 0, 90, color, thickness)
            cv2.ellipse(img, (x2 - corner_radius, y2 - corner_radius), (corner_radius, corner_radius), 0, 0, 90, color, thickness)

    # Mempersiapkan soket untuk mengirim data ke Unity
    server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    server_socket.bind(('localhost', 54321))  # Bind to localhost on port 12345
    server_socket.listen(1)  # Listen for incoming connections
    print("Waiting for Unity to connect...")

    while True:
        try:
            client_socket = None
            while client_socket is None:
                try:
                    client_socket, addr = server_socket.accept()  # Wait for connection from Unity
                    print(f"Connected to {addr}")
                except Exception as e:
                    print(f"Error accepting connection: {e}")
                    client_socket = None

            while True:
                ret, frame = video_capture.read()
                if not ret:
                    print("Failed to grab frame")
                    break

                gray = cv2.cvtColor(frame, cv2.COLOR_BGR2GRAY)
                rects = face_detect(gray, 1)
                emotions = []

                if len(rects) > 0:  # Check if any face detected
                    # Find the closest face by comparing the size of the bounding box
                    closest_rect = max(rects, key=lambda rect: rect.width() * rect.height())
                    shape = predictor_landmarks(gray, closest_rect)
                    shape = face_utils.shape_to_np(shape)
                    (x, y, w, h) = face_utils.rect_to_bb(closest_rect)

                    # Gambar kotak dengan sudut tumpul dan warna biru
                    draw_rounded_rectangle(frame, (x, y), (x + w, y + h), (255, 0, 0), 2)  # Warna biru

                    # Zoom on extracted face
                    face = gray[y:y + h, x:x + w]

                    if face.shape[0] == 0 or face.shape[1] == 0:
                        continue  # Skip if face is not valid

                    face = zoom(face, (shape_x / face.shape[0], shape_y / face.shape[1]))
                    face = face.astype(np.float32) / float(face.max())
                    face = np.reshape(face.flatten(), (1, 48, 48, 1))

                    # Make prediction
                    prediction = model.predict(face)
                    prediction_result = np.argmax(prediction)

                    # Display emotion label with color biru
                    emotion_label = ["Marah", "Jijik", "Takut", "Senang", "Sedih", "Kaget", "Netral"][prediction_result]
                    draw_text_with_shadow(frame, emotion_label, x, y - 10, cv2.FONT_HERSHEY_SIMPLEX, 1.2, (255, 0, 0), 2)  # Warna biru

                    # Simpan emosi ke dalam list
                    emotions.append(emotion_label)

                # Kirim data ke Unity
                try:
                    data = json.dumps(emotions)  # Serialize data to JSON
                    client_socket.sendall(data.encode('utf-8'))  # Send data
                except (BrokenPipeError, ConnectionResetError, socket.error) as e:
                    print(f"Unity disconnected: {e}, waiting for reconnect...")
                    client_socket.close()
                    client_socket = None  # Reset client_socket to wait for reconnection
                    break  # Keluar dari loop dalam dan kembali menunggu koneksi baru

                cv2.imshow('Deteksi Emosi', frame)

                if cv2.waitKey(1) & 0xFF == ord('q'):
                    break

            if cv2.waitKey(1) & 0xFF == ord('q'):
                break

        except Exception as e:
            print(f"Error: {e}")
            break

    # Bersihkan
    video_capture.release()
    cv2.destroyAllWindows()
    server_socket.close()

if __name__ == "__main__":
    show_webcam()
