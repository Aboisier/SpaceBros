using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class PlanetNameGenerator : MonoBehaviour {

    const float NUMBER_AT_BEGINNING_PROB = 0.1f;
    const float LETTER_AT_END_PROB = 0.1f;
    const float NUMBER_AT_END_PROB = 0.1f;

    StreamReader SrSyllabes;
    string badSyllabes;

    int nbNames;
    public string currentSyllabes = "";

    // Use this for initialization
    void Start() {
        SrSyllabes = new StreamReader("syllabes.txt");
        nbNames = int.Parse(SrSyllabes.ReadLine());

        badSyllabes = System.IO.File.ReadAllText("BadSyllabes.txt");
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            GetComponent<Text>().text = GenerateName();
        }
    }

    string ReadAtLine(int n, StreamReader sr)
    {
        sr.DiscardBufferedData();
        sr.BaseStream.Position = 0;

        for (int i = 0; i < n; ++i)
            SrSyllabes.ReadLine();

        return sr.ReadLine();
    }

    bool Probability(float probability)
    {
        return Random.Range(0f, 10000f) / 10000 < probability;
    }

    bool IsGoodName(string name)
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

    string GenerateName()
    {
        // Initialisation
        string name = string.Empty;
        string end = string.Empty;
        string temp = string.Empty;

        // Generates the name
        int n = Random.Range(2, 4); // Number of syllabes
        for (int i = 0; i < n; ++i)
        {
            do
            {
                temp = ReadAtLine(Random.Range(1, nbNames + 1), SrSyllabes);
            } while (!IsGoodName(name + temp));

            name += temp;
        }

        currentSyllabes = name; // For learning purpose

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

    void OnApplicationQuit()
    {
        SrSyllabes.Close();
    }
}
