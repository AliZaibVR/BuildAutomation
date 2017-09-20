using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System;
using System.IO;

public class BuildAutomation {

    [MenuItem("Build/BuildForWindows")]
    public static void BuildWindowsStandalone()
    {
        string path = EditorUtility.SaveFolderPanel("Choose Location of Built Client", "", "");

        string[] scenes = (from scene in EditorBuildSettings.scenes
                           where scene.enabled
                           select scene.path).ToArray();

        // Build player.
        BuildPipeline.BuildPlayer(scenes, path + "/Arthur.exe", BuildTarget.StandaloneWindows, BuildOptions.None);
    }

    public static void BuildClientCMD()
    {
        Debug.LogError("buildClientCMD");
        var outputDir = GetArg("-outputDir");

        if (outputDir == null)
        {
            throw new ArgumentException("No output folder specified.");
        }

        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }
        string[] scenes = (from scene in EditorBuildSettings.scenes
                           where scene.enabled
                           select scene.path).ToArray();
        // Build player.
        Debug.LogError("build path: "+outputDir);
        BuildPipeline.BuildPlayer(scenes, outputDir + "/myPlayer.exe",
                                  BuildTarget.StandaloneWindows,
                                  BuildOptions.None
        );
    }
    private static string GetArg(string name)
    {
        var args = System.Environment.GetCommandLineArgs();
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == name && args.Length > i + 1)
            {
                return args[i + 1];
            }
        }
        return null;
    }
}
