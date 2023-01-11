// Description
// Date: 23/01/11
// Converse TextAsset(.tsv) to ScriptableObject.
// Code source: https://blog.unity.com/technology/advanced-editor-scripting-hacks-to-save-you-time-part-2

[ScriptedImporter(0, "tsv")]
public class UnitStatsImporter : ScriptedImporter
{
    public override void OnImportAsset(AssetImportContext ctx)
    {
        var file = File.OpenText(assetPath);

        // Check if this is a Unit
        bool isUnit = !assetPath.Contains("Buildings");

        // The first line is the header, ignore
        file.ReadLine();

        var main = new TextAsset();
        ctx.AddObjectToAsset("Main", main);
        ctx.SetMainObject(main);

        while (!file.EndOfStream)
        {
            // Read the line and divide at the tabs
            var line = file.ReadLine();
            var lineElements = line.Split('\t');

            var name = lineElements[0].ToLower();
            if (isUnit)
            {
                // Fill all the values
                var entry = ScriptableObject.CreateInstance<SoldierStats>();
                entry.name = name;
                entry.HP = float.Parse(lineElements[1]);
                entry.attack = float.Parse(lineElements[2]);
                entry.defense = float.Parse(lineElements[3]);
                entry.attackRatio = float.Parse(lineElements[4]);
                entry.attackRange = float.Parse(lineElements[5]);
                entry.viewRange = float.Parse(lineElements[6]);
                entry.speed = float.Parse(lineElements[7]);

                ctx.AddObjectToAsset(name, entry);
            }
            else
            {
                // Fill all the values
                var entry = ScriptableObject.CreateInstance<BuildingStats>();
                entry.name = name;
                entry.HP = float.Parse(lineElements[1]);

                ctx.AddObjectToAsset(name, entry);
            }
        }

        // Close the file
        file.Close();
    }
}