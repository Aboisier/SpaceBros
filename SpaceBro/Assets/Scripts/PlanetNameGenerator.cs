using UnityEngine;
using System.IO;
using UnityEngine.UI;

public static class PlanetNameGenerator {

    const float NUMBER_AT_BEGINNING_PROB = 0.1f;
    const float LETTER_AT_END_PROB = 0.1f;
    const float NUMBER_AT_END_PROB = 0.1f;
    const int MIN_SYLLABES_NB = 2;
    const int MAX_SYLLABES_NB = 3;

    static StreamReader SrSyllabes;
    static string badSyllabes;
    static int nbNames;

    // Use this for initialization
    static PlanetNameGenerator() {
        SrSyllabes = new StreamReader("syllabes.txt");
        nbNames = int.Parse(SrSyllabes.ReadLine());

        badSyllabes = System.IO.File.ReadAllText("BadSyllabes.txt");
    }

    static string ReadAtLine(int n, StreamReader sr)
    {
        sr.DiscardBufferedData();
        sr.BaseStream.Position = 0;

        for (int i = 0; i < n; ++i)
            SrSyllabes.ReadLine();

        return sr.ReadLine();
    }

    static bool Probability(float probability)
    {
        return Random.Range(0f, 10000f) / 10000 < probability;
    }

    static bool IsGoodName(string name)
    {
        for (int i = 2; i < name.Length; ++i)
        {
            for (int j = 0; j < name.Length - i; ++j)
            {
                 if (badSyllabes.Contains(" " + name.Substring(j, i) + " "))
                    return false;
            }
        }

        return true;
    }

    public static string GenerateName()
    {
        // Initialisation
        string name = string.Empty;
        string end = string.Empty;
        string temp = string.Empty;

        // Generates the name
        int n = Random.Range(MIN_SYLLABES_NB, MAX_SYLLABES_NB); // Number of syllabes

        for (int i = 0; i < n; ++i)
        {
            do
            {
                temp = ReadAtLine(Random.Range(1, nbNames + 1), SrSyllabes);
            } while (!IsGoodName(name + temp));

            name += temp;
        }

        // Generates the end and adds cool stuff
        if (Probability(NUMBER_AT_END_PROB))
            end += Random.Range(1, 10);
        if (Probability(LETTER_AT_END_PROB))
            end += (char)Random.Range(65, 90);

        // Formatting and stuff like this
        if (!string.IsNullOrEmpty(name))
            name = name[0].ToString().ToUpper() + name.Substring(1);
        if (!string.IsNullOrEmpty(end))
            end = "-" + end;

        return name + end;
    }
}
