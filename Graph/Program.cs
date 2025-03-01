// See https://aka.ms/new-console-template for more information

using System.Drawing;
using Graph;

Console.WriteLine("Hello, World!");
//List<Point> points = new List<Point>();
//List<Noeud<string>> noeuds = new List<Noeud<string>>();
//Graphe g1 = new Graphe<T>("titre", noeuds, points);

static void AfficherMatrice(int[,] mat)
{
    for (int i = 0; i < mat.GetLength(0); i++)
    {
        for (int j = 0; j < mat.GetLength(1); j++)
        {
            if (mat[i, j] < 10)
            {
                Console.Write(mat[i, j] + "  ");
            }
            else
            {
                Console.Write(mat[i, j] + " ");
            }
        }
        Console.WriteLine();
    }
}

static void AfficherFichier()
{
    string[] lines = File.ReadAllLines("..\\..\\..\\soc-karate.mtx").Skip(24).ToArray();
    foreach (string line in lines)
    {
        Console.WriteLine(line);
        Console.ReadKey();
    }
}

//AfficherFichier();

Graphe<string> g1 = new Graphe<string>("g1") { Titre = "Karate" };

g1.RemplirGraphe();
g1.LiensGraphe();
g1.AfficherListeAdjacense();
AfficherMatrice(g1.CreerMatriceAdjacense());