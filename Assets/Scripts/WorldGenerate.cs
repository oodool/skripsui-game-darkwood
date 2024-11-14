//using System.Collections.Generic;
//using UnityEngine;
//using TMPro;

//public class WorldGenerate : MonoBehaviour
//{
//    public TextMeshProUGUI[] entropyTexts; // Array untuk menampung UI teks entropi
//    public Sprite[] tileSprites; // Array untuk menampung sprites tile
//    public SpriteRenderer[,] tileRenderers; // Grid dari SpriteRenderer untuk menggambar dunia
//    public World world; // Referensi ke kelas World yang berisi data dunia
//    public int tileSize = 32; // Ukuran tile dalam pixel
//    public float scaleTile = 1.0f; // Faktor skala tile

//    private void Start()
//    {
//        // Inisialisasi grid dari SpriteRenderer
//        tileRenderers = new SpriteRenderer[world.WorldWidth, world.WorldHeight];
//        for (int x = 0; x < world.WorldWidth; x++)
//        {
//            for (int y = 0; y < world.WorldHeight; y++)
//            {
//                GameObject tileObject = new GameObject($"Tile_{x}_{y}");
//                SpriteRenderer renderer = tileObject.AddComponent<SpriteRenderer>();
//                tileRenderers[x, y] = renderer;
//                tileObject.transform.SetParent(transform); // Mengatur parent ke objek ini
//            }
//        }
//    }

//    public void UpdateWorld()
//    {
//        int lowestEntropy = world.GetLowestEntropy();

//        for (int y = 0; y < world.WorldHeight; y++)
//        {
//            for (int x = 0; x < world.WorldWidth; x++)
//            {
//                int tileEntropy = world.GetEntropy(x, y);
//                int tileType = world.GetType(x, y);

//                // Mengubah ukuran tile
//                Vector3 scale = new Vector3(scaleTile, scaleTile, 1);
//                tileRenderers[x, y].transform.localScale = scale;

//                // Menampilkan entropi
//                if (tileEntropy > 0)
//                {
//                    UpdateEntropyText(x, y, tileEntropy);
//                    tileRenderers[x, y].sprite = null; // Kosongkan sprite jika entropi lebih dari 0
//                }
//                else
//                {
//                    // Mendapatkan sprite berdasarkan tileType
//                    if (tileType < tileSprites.Length)
//                    {
//                        tileRenderers[x, y].sprite = tileSprites[tileType];
//                    }
//                    else
//                    {
//                        tileRenderers[x, y].sprite = null; // Atur null jika tidak valid
//                    }
//                }

//                // Menentukan posisi tile di dunia
//                tileRenderers[x, y].transform.localPosition = new Vector3(x * tileSize * scaleTile, y * tileSize * scaleTile, 0);
//            }
//        }
//    }

//    private void UpdateEntropyText(int x, int y, int entropy)
//    {
//        // Update UI teks entropi pada posisi yang sesuai
//        if (entropyTexts != null && x < entropyTexts.Length && y < entropyTexts.Length)
//        {
//            entropyTexts[x].text = entropy.ToString();
//            // Atur posisi teks jika diperlukan
//            entropyTexts[x].transform.localPosition = new Vector3(x * tileSize * scaleTile, y * tileSize * scaleTile, 0);
//        }
//    }
//}
