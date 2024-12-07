using System.IO;
using UnityEngine;

public static class PathHelper
{
    // 현재 Unity 프로젝트의 Assets 폴더 상위 디렉토리 경로 반환
    public static string GetProjectFolder()
    {
        return Directory.GetParent(Application.dataPath).FullName;
    }

    // 상위 상위 디렉토리 경로 반환 (Assets의 두 단계 위)
    public static string GetPojectParentFolder()
    {
        return Directory.GetParent(GetProjectFolder()).FullName;
    }

    static string GetApplicationDataPath
    {
        get { return Application.dataPath; }
    }
    public static string GetFolderPath(string saveFolder)
    {
        return GetApplicationDataPath + '/' + saveFolder;
    }
    public static string GetFilePath(string saveFolder, string fileName)
    {
        return GetFolderPath(saveFolder) + '/' + fileName;
    }
}