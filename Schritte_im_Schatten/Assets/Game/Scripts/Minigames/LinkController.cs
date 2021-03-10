using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;
using TMPro.Examples;
using I2.Loc;

public class LinkController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

    private bool isHoveringObject;
    private int m_selectedLink = -1;
    private int m_lastIndex = -1;

    private TextMeshProUGUI m_TextMeshPro;
    private Camera m_Camera;
    private TMP_MeshInfo[] m_cachedMeshInfoVertexData;
    private UI_S5 ui;

    // Use this for initialization
    void Awake () {
        m_TextMeshPro = gameObject.GetComponent<TextMeshProUGUI>();
        m_Camera = null;
        ui = Camera.main.GetComponent<UI_S5>();
    }

    void OnEnable()
    {
        // Subscribe to event fired when text object has been regenerated.
        TMPro_EventManager.TEXT_CHANGED_EVENT.Add(ON_TEXT_CHANGED);
    }

    void OnDisable()
    {
        // UnSubscribe to event fired when text object has been regenerated.
        TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(ON_TEXT_CHANGED);
    }


    void ON_TEXT_CHANGED(Object obj)
    {
        if (obj == m_TextMeshPro)
        {
            // Update cached vertex data.
            m_cachedMeshInfoVertexData = m_TextMeshPro.textInfo.CopyMeshInfoVertexData();
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (isHoveringObject)
        {

            #region Example of Link Handling
            // Check if mouse intersects with any links.
            int linkIndex = TMP_TextUtilities.FindIntersectingLink(m_TextMeshPro, Input.mousePosition, m_Camera);

            // Clear previous link selection if one existed.
            if (m_selectedLink != -1 && (linkIndex == -1   || linkIndex != m_selectedLink))
            {
               
                TMP_LinkInfo linkInfo = m_TextMeshPro.textInfo.linkInfo[m_selectedLink];

                // Iterate through each of the characters of the word.
                for (int i = 0; i < linkInfo.linkTextLength; i++)
                {
                    int characterIndex = linkInfo.linkTextfirstCharacterIndex +i;

                    // Get the index of the material / sub text object used by this character.
                    int meshIndex = m_TextMeshPro.textInfo.characterInfo[characterIndex].materialReferenceIndex;

                    // Get the index of the first vertex of this character.
                    int vertexIndex = m_TextMeshPro.textInfo.characterInfo[characterIndex].vertexIndex;

                    // Get a reference to the vertex color
                    Color32[] vertexColors = m_TextMeshPro.textInfo.meshInfo[meshIndex].colors32;

                    Color32 c = new Color32(0, 0, 0, 255);
                    //Color32 c = vertexColors[vertexIndex + 0].Tint(1.33333f);

                    vertexColors[vertexIndex + 0] = c;
                    vertexColors[vertexIndex + 1] = c;
                    vertexColors[vertexIndex + 2] = c;
                    vertexColors[vertexIndex + 3] = c;
                }

                // Update Geometry
                m_TextMeshPro.UpdateVertexData(TMP_VertexDataUpdateFlags.All);
                m_selectedLink = -1;
            }

            // Handle new Link selection.
            if (linkIndex != -1 && linkIndex != m_selectedLink)
            {
                m_selectedLink = linkIndex;

                TMP_LinkInfo linkInfo = m_TextMeshPro.textInfo.linkInfo[linkIndex];
                for (int i = 0; i < linkInfo.linkTextLength; i++)
                {
                    int characterIndex = linkInfo.linkTextfirstCharacterIndex + i;

                    // Get the index of the material / sub text object used by this character.
                    int meshIndex = m_TextMeshPro.textInfo.characterInfo[characterIndex].materialReferenceIndex;

                    int vertexIndex = m_TextMeshPro.textInfo.characterInfo[characterIndex].vertexIndex;

                    // Get a reference to the vertex color
                    Color32[] vertexColors = m_TextMeshPro.textInfo.meshInfo[meshIndex].colors32;

                    Color32 c = new Color32(255, 0, 0, 255);
                    //Color32 c = vertexColors[vertexIndex + 0].Tint(0.75f);

                    vertexColors[vertexIndex + 0] = c;
                    vertexColors[vertexIndex + 1] = c;
                    vertexColors[vertexIndex + 2] = c;
                    vertexColors[vertexIndex + 3] = c;
                }
                m_TextMeshPro.UpdateVertexData(TMP_VertexDataUpdateFlags.All);
            }
            #endregion

        }
        else
        {
            // Restore any character that may have been modified
            if (m_lastIndex != -1)
            {
                RestoreCachedVertexAttributes(m_lastIndex);
                m_lastIndex = -1;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("OnPointerEnter()");
        isHoveringObject = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("OnPointerExit()");
        isHoveringObject = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log("Click at POS: " + eventData.position + "  World POS: " + eventData.worldPosition);

        // Check if Mouse Intersects any of the characters. If so, assign a random color.

        #region Link Selection Handling
        
        // Check if Mouse intersects any words and if so assign a random color to that word.
        int linkIndex = TMP_TextUtilities.FindIntersectingLink(m_TextMeshPro, Input.mousePosition, m_Camera);
        if (linkIndex != -1)
        {
            TMP_LinkInfo linkInfo = m_TextMeshPro.textInfo.linkInfo[linkIndex];
            int linkHashCode = linkInfo.hashCode;

            //Debug.Log(TMP_TextUtilities.GetSimpleHashCode("id_02"));

            switch (linkInfo.GetLinkID())
            {
                case "271B":
                    //m_TextMeshPro.pageToDisplay = 1;
                    m_TextMeshPro.text = ScriptLocalization.AktenSpiel.Akte_271_B;
                    break;
                case "Juni":
                    //m_TextMeshPro.pageToDisplay = 2;
                    m_TextMeshPro.text = ScriptLocalization.AktenSpiel._17Juni;
                    break;
                case "Pizza":
                    //m_TextMeshPro.pageToDisplay = 3;
                    m_TextMeshPro.text = ScriptLocalization.AktenSpiel.Pizzafix;
                    break;
                case "Presse":
                    //m_TextMeshPro.pageToDisplay = 4;
                    m_TextMeshPro.text = ScriptLocalization.AktenSpiel.Pressekonferenz;
                    break;
                case "SOKO":
                    //m_TextMeshPro.pageToDisplay = 5;
                    m_TextMeshPro.text = ScriptLocalization.AktenSpiel.Sonderkommision;
                    break;
                case "Verdächtige":
                    //m_TextMeshPro.pageToDisplay = 6;
                    m_TextMeshPro.text = ScriptLocalization.AktenSpiel.Verdacht;
                    break;
                case "Interview":
                    //m_TextMeshPro.pageToDisplay = 7;
                    m_TextMeshPro.text = ScriptLocalization.AktenSpiel.Interview;
                    break;
                case "Polizist":
                    //m_TextMeshPro.pageToDisplay = 8;
                    m_TextMeshPro.text = ScriptLocalization.AktenSpiel.Polizienachwuchs;
                    break;
                case "Guenther":
                    //m_TextMeshPro.pageToDisplay = 9;
                    m_TextMeshPro.text = ScriptLocalization.AktenSpiel.Guenther;
                    break;
                case "Ernst":
                    //m_TextMeshPro.pageToDisplay = 10;
                    m_TextMeshPro.text = ScriptLocalization.AktenSpiel.Ernst;
                    break;
                case "Zobel":
                    //m_TextMeshPro.pageToDisplay = 11;
                    m_TextMeshPro.text = ScriptLocalization.AktenSpiel.Zobel;
                    break;
                case "Neu":
                    //m_TextMeshPro.pageToDisplay = 12;
                    m_TextMeshPro.text = ScriptLocalization.AktenSpiel.Neueinsteiger;
                    break;
                case "InterviewRene":
                    //m_TextMeshPro.pageToDisplay = 13;
                    m_TextMeshPro.text = ScriptLocalization.AktenSpiel.InterviewRene;
                    break;
                case "Rene":
                    //m_TextMeshPro.pageToDisplay = 14;
                    m_TextMeshPro.text = ScriptLocalization.AktenSpiel.Final;
                    break;
                case "Zurück":
                    StartCoroutine(ui.disableAktenspiel());
                    break;

            }

           
        }
        
        #endregion
    }

    void RestoreCachedVertexAttributes(int index)
    {
        if (index == -1 || index > m_TextMeshPro.textInfo.characterCount - 1) return;

        // Get the index of the material / sub text object used by this character.
        int materialIndex = m_TextMeshPro.textInfo.characterInfo[index].materialReferenceIndex;

        // Get the index of the first vertex of the selected character.
        int vertexIndex = m_TextMeshPro.textInfo.characterInfo[index].vertexIndex;

        // Restore Vertices
        // Get a reference to the cached / original vertices.
        Vector3[] src_vertices = m_cachedMeshInfoVertexData[materialIndex].vertices;

        // Get a reference to the vertices that we need to replace.
        Vector3[] dst_vertices = m_TextMeshPro.textInfo.meshInfo[materialIndex].vertices;

        // Restore / Copy vertices from source to destination
        dst_vertices[vertexIndex + 0] = src_vertices[vertexIndex + 0];
        dst_vertices[vertexIndex + 1] = src_vertices[vertexIndex + 1];
        dst_vertices[vertexIndex + 2] = src_vertices[vertexIndex + 2];
        dst_vertices[vertexIndex + 3] = src_vertices[vertexIndex + 3];

        // Restore Vertex Colors
        // Get a reference to the vertex colors we need to replace.
        Color32[] dst_colors = m_TextMeshPro.textInfo.meshInfo[materialIndex].colors32;

        // Get a reference to the cached / original vertex colors.
        Color32[] src_colors = m_cachedMeshInfoVertexData[materialIndex].colors32;

        // Copy the vertex colors from source to destination.
        dst_colors[vertexIndex + 0] = src_colors[vertexIndex + 0];
        dst_colors[vertexIndex + 1] = src_colors[vertexIndex + 1];
        dst_colors[vertexIndex + 2] = src_colors[vertexIndex + 2];
        dst_colors[vertexIndex + 3] = src_colors[vertexIndex + 3];

        // Restore UV0S
        // UVS0
        Vector2[] src_uv0s = m_cachedMeshInfoVertexData[materialIndex].uvs0;
        Vector2[] dst_uv0s = m_TextMeshPro.textInfo.meshInfo[materialIndex].uvs0;
        dst_uv0s[vertexIndex + 0] = src_uv0s[vertexIndex + 0];
        dst_uv0s[vertexIndex + 1] = src_uv0s[vertexIndex + 1];
        dst_uv0s[vertexIndex + 2] = src_uv0s[vertexIndex + 2];
        dst_uv0s[vertexIndex + 3] = src_uv0s[vertexIndex + 3];

        // UVS2
        Vector2[] src_uv2s = m_cachedMeshInfoVertexData[materialIndex].uvs2;
        Vector2[] dst_uv2s = m_TextMeshPro.textInfo.meshInfo[materialIndex].uvs2;
        dst_uv2s[vertexIndex + 0] = src_uv2s[vertexIndex + 0];
        dst_uv2s[vertexIndex + 1] = src_uv2s[vertexIndex + 1];
        dst_uv2s[vertexIndex + 2] = src_uv2s[vertexIndex + 2];
        dst_uv2s[vertexIndex + 3] = src_uv2s[vertexIndex + 3];


        // Restore last vertex attribute as we swapped it as well
        int lastIndex = (src_vertices.Length / 4 - 1) * 4;

        // Vertices
        dst_vertices[lastIndex + 0] = src_vertices[lastIndex + 0];
        dst_vertices[lastIndex + 1] = src_vertices[lastIndex + 1];
        dst_vertices[lastIndex + 2] = src_vertices[lastIndex + 2];
        dst_vertices[lastIndex + 3] = src_vertices[lastIndex + 3];

        // Vertex Colors
        src_colors = m_cachedMeshInfoVertexData[materialIndex].colors32;
        dst_colors = m_TextMeshPro.textInfo.meshInfo[materialIndex].colors32;
        dst_colors[lastIndex + 0] = src_colors[lastIndex + 0];
        dst_colors[lastIndex + 1] = src_colors[lastIndex + 1];
        dst_colors[lastIndex + 2] = src_colors[lastIndex + 2];
        dst_colors[lastIndex + 3] = src_colors[lastIndex + 3];

        // UVS0
        src_uv0s = m_cachedMeshInfoVertexData[materialIndex].uvs0;
        dst_uv0s = m_TextMeshPro.textInfo.meshInfo[materialIndex].uvs0;
        dst_uv0s[lastIndex + 0] = src_uv0s[lastIndex + 0];
        dst_uv0s[lastIndex + 1] = src_uv0s[lastIndex + 1];
        dst_uv0s[lastIndex + 2] = src_uv0s[lastIndex + 2];
        dst_uv0s[lastIndex + 3] = src_uv0s[lastIndex + 3];

        // UVS2
        src_uv2s = m_cachedMeshInfoVertexData[materialIndex].uvs2;
        dst_uv2s = m_TextMeshPro.textInfo.meshInfo[materialIndex].uvs2;
        dst_uv2s[lastIndex + 0] = src_uv2s[lastIndex + 0];
        dst_uv2s[lastIndex + 1] = src_uv2s[lastIndex + 1];
        dst_uv2s[lastIndex + 2] = src_uv2s[lastIndex + 2];
        dst_uv2s[lastIndex + 3] = src_uv2s[lastIndex + 3];

        // Need to update the appropriate 
        m_TextMeshPro.UpdateVertexData(TMP_VertexDataUpdateFlags.All);
    }

}
