using System.IO;
using UnityEngine;

public static class PathHelper
{
    // ���� Unity ������Ʈ�� Assets ���� ���� ���丮 ��� ��ȯ
    public static string GetProjectFolder()
    {
        return Directory.GetParent(Application.dataPath).FullName;
    }

    // ���� ���� ���丮 ��� ��ȯ (Assets�� �� �ܰ� ��)
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