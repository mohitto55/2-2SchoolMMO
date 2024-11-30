using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class CharMesh
{
    public enum ShowType
    {
        Blink,
        Floating,
    }
    [Flags]
    public enum AnimType
    {
        Shake    = 1,
        Wave     = 2,
    }
    [SerializeField]
    private AnimType m_animType;
    [SerializeField]
    private float m_animOffset;
    [SerializeField]
    private float m_showOffset;
    private bool m_active;

    public bool Active { get => m_active; }


    private Color32[] m_colors = new Color32[4];
    private Vector3[] m_vertices = new Vector3[4];

    public Color32[] colors { get => m_colors; }
    public Vector3[] vertices { get => m_vertices; }
    
    private Color32[] m_originColor = new Color32[4];
    private Vector3[] m_originVertices = new Vector3[4];

    private Color32[] m_targetColor = new Color32[4];
    private Vector3[] m_targetVertices = new Vector3[4];

    float m_size;
    int m_startIdx;
    public int startIdx { get => m_startIdx; }
    char m_character;
    public char character { get => m_character; }

    public CharMesh(char character, int startIdx, Vector3[] pos, Color32[] color, float size = 1)
    {

        m_character = character;
        m_startIdx = startIdx;

        for (int i = 0; i < 4; i++)
        {
            m_vertices[i]       = pos[i];
            m_colors[i]         = color[i];
            m_originVertices[i] = pos[i];
            m_originColor[i]    = color[i];
            m_targetVertices[i] = pos[i];
            m_targetColor[i] = color[i];
        }

        InitCharMesh();
    }
    public void CharMeshUpdate()
    {
        if (!m_active) return;
        if (m_character == '\n') return;
        var shakeVertex = new Vector3(
            UnityEngine.Random.Range(-m_animOffset, m_animOffset),
            UnityEngine.Random.Range(-m_animOffset, m_animOffset), 0);
        for (int j = 0; j < 4; j++)
        {       
            colors[j] = Color32.Lerp(colors[j], m_targetColor[j], Time.deltaTime * 2);
            vertices[j] = Vector3.Lerp(vertices[j], m_targetVertices[j], Time.deltaTime * 3);


            if (m_animType.HasFlag(AnimType.Shake))
            {
                vertices[j] = m_originVertices[j] + shakeVertex;
            }
            if (m_animType.HasFlag(AnimType.Wave))
            {
                vertices[j] = m_originVertices[j] + new Vector3(
                    0,
                    Mathf.Sin((Time.time + (m_startIdx )) *2) * m_animOffset , 0);
            }
        }
        


    }
    private void InitCharMesh()
    {

        for (int i = 0; i < 4; i++)
        {
            vertices[i] = m_originVertices[i] + Vector3.down * 10;
            m_targetVertices[i] = vertices[i];
            colors[i] = new Color32(m_originColor[i].r, m_originColor[i].g, m_originColor[i].b, 0);
            m_targetColor[i] = colors[i];
        }
    }
    public void Show(ShowType showType, AnimType animType,float showOffset = 1, float animOffset = 0)
    {

        m_active = true;
        m_animType = animType;
        m_showOffset = showOffset;
        m_animOffset = animOffset;

        switch (showType)
        {
            case ShowType.Blink:
                for (int i = 0; i < 4; i++)
                {
                    colors[i] = m_originColor[i];
                    vertices[i] = m_originVertices[i];
                    m_targetColor[i] = m_originColor[i];
                    m_targetVertices[i] = m_originVertices[i];
                }
                break;
            case ShowType.Floating:
                for (int i = 0; i < 4; i++)
                {
                    m_targetColor[i] = m_originColor[i];
                    m_targetVertices[i] = m_originVertices[i];
                }
                break;
            default:
                break;
        }




    }
}


[Serializable]
public class TextContainer
{
    public enum Type
    {
        Text,
        Marker,
    }
    [SerializeField]
    Type m_type;
    public Type type { get => m_type; }
    [SerializeField]
    string m_text;
    public string text { get => m_text; }

    public TextContainer(Type type, string text)
    {
        m_type = type;
        m_text = text;
    }
}



[RequireComponent(typeof(TMP_Text))]
public class TMPTextPrinter : MonoBehaviour
{
    public enum State
    {
        Ready,
        Ing,
        End,
    }


    State m_state;
    public State state { get => m_state; }


    [SerializeField]
    List<TextContainer> m_msgs = new List<TextContainer>();
    [SerializeField]
    List<CharMesh> m_textMesh = new List<CharMesh>();

    [SerializeField, TextArea]
    private string m_dialogue;
    [SerializeField]
    private TMP_Text m_tmpText;

    [SerializeField]
    private float m_floatingDelay = 1f;

    private float m_animOffset;
    private float m_showOffset;

    private int m_printingIdx;

    private CharMesh.AnimType m_curAnimType;
    private CharMesh.ShowType m_curShowType;

    private bool m_boxPrint = false;

    private float m_textSize = 1;



    private void Awake()
    {
        m_tmpText = GetComponent<TMP_Text>();



    }
    private void OnDisable()
    {
        InitText(""); 
    }
    bool m_init;
    public void InitText(string text, int a = 0)
    {
        
        StartCoroutine(InitText(text));
    }
    IEnumerator InitText(string text)
    {
        m_state = State.Ready;

        Debug.Log(text);
        m_animOffset = 0;
        m_showOffset = 0;
        m_printingIdx = 0;

        m_curAnimType = 0;
        m_curShowType = CharMesh.ShowType.Blink;

        m_msgs.Clear();
        m_textMesh.Clear();

        m_tmpText.text = "";
        m_dialogue = text;

        GenerateText();
        var color = m_tmpText.color;

        color.a = 0;
        m_tmpText.color = color;
        m_tmpText.ForceMeshUpdate(true);

        yield return new WaitForEndOfFrame();

        color.a = 1;
        m_tmpText.color = color;
        m_tmpText.ForceMeshUpdate(true);

        InitTMPMesh();

        foreach (var item in m_textMesh)
        {
            item.CharMeshUpdate();
        }
        UpdateTMPVertexData();

        if (text == "")
        {
            m_init = false;
        }
        else
        {
            m_init = true;
        }
    }

    private void GenerateText()
    {
        string marker = "";
        string text = "";

        for (int i = 0; i < m_dialogue.Length; i++)
        {
            if(m_dialogue[i] == '[')
            {
                i++;
                // Read Type
                while (m_dialogue[i] != ']')
                {
                    if(m_dialogue.Length <= i)
                    {
                        Debug.Log("didn't find end point ']'");
                        break;
                    }

                    marker += m_dialogue[i];

                    i++;
                }
                m_msgs.Add(new TextContainer(TextContainer.Type.Text, text));
                m_msgs.Add(new TextContainer(TextContainer.Type.Marker, marker));

                m_tmpText.text += text;

                text = "";
                marker = "";
            }
            else if (m_dialogue[i] == '<')
            {
                // Read Type
                while (m_dialogue[i] != '>')
                {
                    if (m_dialogue.Length <= i)
                    {
                        Debug.Log("didn't find end point '>'");
                        break;
                    }

                    text += m_dialogue[i];
                    i++;
                }
                text += m_dialogue[i];
                m_tmpText.text += text;

                text = "";
                marker = "";
            }
            else
            {
                text += m_dialogue[i];
            }
        }
        m_tmpText.text += text;
        m_msgs.Add(new TextContainer(TextContainer.Type.Text, text));
    }

    Coroutine m_dgRoutine;
    Coroutine m_dgPrintRoutine;
    [ContextMenu("Print")]
    public Coroutine PrintDialogue()
    {
        m_state = State.Ing;
        m_dgRoutine = StartCoroutine(DialogueRoutine());

        return m_dgRoutine;
    }
    IEnumerator DialogueRoutine()
    {
        for (int i = 0; i < m_msgs.Count; i++)
        {
            switch (m_msgs[i].type)
            {
                case TextContainer.Type.Text:
                    m_dgPrintRoutine = StartCoroutine(PrintText(m_msgs[i].text));
                    yield return m_dgPrintRoutine;
                    break;
                case TextContainer.Type.Marker:
                    ReadMarker(m_msgs[i].text);
                    break;
                default:
                    break;
            }
        }
        m_state = State.End;
    }
    public void SkipDialogue()
    {
        StopCoroutine(m_dgRoutine);
        StopCoroutine(m_dgPrintRoutine);
        m_printingIdx = 0;
        for (int i = 0; i < m_msgs.Count; i++)
        {
            switch (m_msgs[i].type)
            {
                case TextContainer.Type.Text:
                    if (!m_boxPrint)
                    {
                        for (int j = 0; j < m_msgs[i].text.Length; j++)
                        {
                            m_textMesh[m_printingIdx].Show(m_curShowType, m_curAnimType, m_showOffset, m_animOffset);
                            m_printingIdx++;
                        }
                    }
                    else
                    {
                        for (int j = 0; j < m_msgs[i ].text.Length; j++)
                        {
                            m_textMesh[m_printingIdx].Show(m_curShowType, m_curAnimType, m_showOffset, m_animOffset);
                            m_printingIdx++;
                        }
                    }
                    break;
                case TextContainer.Type.Marker:
                    ReadMarker(m_msgs[i].text);
                    break;
                default:
                    break;
            }
        }

        m_state = State.End;
    }
    IEnumerator PrintText(string text)
    {
        yield return null;
        if (!m_boxPrint)
        {
            for (int i = 0; i < text.Length; i++)
            {
                yield return new WaitForSeconds(m_floatingDelay);
                m_textMesh[m_printingIdx].Show(m_curShowType, m_curAnimType, m_showOffset, m_animOffset);
                m_printingIdx++;
            }
        }
        else
        {
            for (int i = 0; i < text.Length; i++)
            {
                m_textMesh[m_printingIdx].Show(m_curShowType, m_curAnimType, m_showOffset, m_animOffset);
                m_printingIdx++;
            }
        }

    }
    private void ReadMarker(string marker)
    {
        marker = marker.Replace(" ", "");
        var str = marker.Split(",");

        switch (str[0].ToLower())
        {
            case "floating":
                m_curShowType = CharMesh.ShowType.Floating;
                break;
            case "blink":
                m_curShowType = CharMesh.ShowType.Blink;
                break;
            case "box":
                m_boxPrint = true;
                break;
            case "/box":
                m_boxPrint = false;
                break;
            case "delay":
                m_floatingDelay = float.Parse(str[1]);
                break;
            case "shake":
                m_animOffset = float.Parse(str[1]);
                m_curAnimType |= CharMesh.AnimType.Shake;
                break;
            case "/shake":
                m_curAnimType &= ~CharMesh.AnimType.Shake; 
                break;
            case "wave":
                m_animOffset = float.Parse(str[1]);
                m_curAnimType |= CharMesh.AnimType.Wave;
                break;
            case "/wave":
                m_curAnimType &= ~CharMesh.AnimType.Wave; 
                break;
            default:
                break;
        }

    }
    public void SkipText()
    {
    
    }
    private void UpdateTMPVertexData()
    {
        m_tmpText.ForceMeshUpdate();

        var newVertices = m_tmpText.mesh.vertices;

        var newColors = m_tmpText.mesh.colors32;

        for (int i = 0; i < m_textMesh.Count; i++)
        {
            if (i != 0&& m_textMesh[i].startIdx == 0)
            {
                continue;
            }
            for (int j = 0; j < 4; j++)
            {

                newVertices[m_textMesh[i].startIdx + j] = m_textMesh[i].vertices[j];
                newColors[m_textMesh[i].startIdx + j] = m_textMesh[i].colors[j];


            }
        }

        m_tmpText.textInfo.meshInfo[0].vertices = newVertices;
        m_tmpText.textInfo.meshInfo[0].colors32 = newColors;
        m_tmpText.UpdateVertexData(TMP_VertexDataUpdateFlags.All);
       // m_tmpText.UpdateGeometry(m_tmpText.mesh, 0);

         
    }
    private void Update()
    {
        if (!m_init) return;
        foreach (var item in m_textMesh)
        {
            item.CharMeshUpdate();
        }
        UpdateTMPVertexData();
    }
    private void Start()
    {
    }
    private void InitTMPMesh()
    {
        foreach (var charInfo in m_tmpText.textInfo.characterInfo)
        {
            var materialIndex = charInfo.materialReferenceIndex;
            Vector3[] vertexContainer = new Vector3[4];
            Color32[] colorContainer = new Color32[4];
            for (int i = 0; i < 4; i++)
            {
                vertexContainer[i] = m_tmpText.textInfo.meshInfo[materialIndex].vertices[charInfo.vertexIndex + i];
                colorContainer[i] = m_tmpText.textInfo.meshInfo[materialIndex].colors32[charInfo.vertexIndex + i];

            }
            var mesh = new CharMesh(charInfo.character, charInfo.vertexIndex, vertexContainer, colorContainer);
            m_textMesh.Add(mesh);


        }

    }
}