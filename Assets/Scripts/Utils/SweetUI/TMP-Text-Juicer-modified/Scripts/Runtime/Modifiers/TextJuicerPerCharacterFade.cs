using TMPro;
using UnityEngine;

namespace BrunoMikoski.TextJuicer.Modifiers
{
	[AddComponentMenu("UI/Text Juicer Modifiers/Per Character Fade Modifier", 11)]
	public sealed class TextJuicerPerCharacterFade : TextJuicerVertexModifier
	{
		public Gradient gradient = new Gradient();

		private Color32[] newVertexColors;

		private Color32 targetColor;

		public override bool ModifyGeometry
		{
			get { return false; }
		}
		public override bool ModifyVertex
		{
			get { return true; }
		}

		protected override void OnReset()
		{
			GradientColorKey[] colorKeys = new GradientColorKey[3];
			GradientAlphaKey[] alphaKeys = new GradientAlphaKey[3];
			colorKeys[0] = new GradientColorKey(Color.black, 0.3f);
			colorKeys[1] = new GradientColorKey(Color.red, 0.4f);
			colorKeys[2] = new GradientColorKey(Color.black, 0.5f);
			alphaKeys[0] = new GradientAlphaKey(0, 0);
			alphaKeys[1] = new GradientAlphaKey(0, 0.35f);
			alphaKeys[2] = new GradientAlphaKey(1, 0.39f);


			gradient.SetKeys(colorKeys, alphaKeys);
		}

		public override void ModifyCharacter(CharacterData characterData, TMP_Text textComponent,
			TMP_TextInfo textInfo,
			float progress,
			TMP_MeshInfo[] meshInfo)
		{
			if (gradient == null)
				return;

			textComponent.color = gradient.Evaluate(0);//to avoid flash of the full text at the first frame of loading the text
			int materialIndex = characterData.MaterialIndex;

			newVertexColors = textInfo.meshInfo[materialIndex].colors32;

			int vertexIndex = characterData.VertexIndex;

			

			targetColor = gradient.Evaluate(characterData.Progress);

			newVertexColors[vertexIndex + 0] = targetColor;
			newVertexColors[vertexIndex + 1] = targetColor;
			newVertexColors[vertexIndex + 2] = targetColor;
			newVertexColors[vertexIndex + 3] = targetColor;
		}
	}
}
