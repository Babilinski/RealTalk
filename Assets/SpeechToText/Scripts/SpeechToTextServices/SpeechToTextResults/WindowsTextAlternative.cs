#if (UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN || UNITY_WINRT || UNITY_WINRT_8_0 || UNITY_WINRT_8_1 || UNITY_WINRT_10_0)
using UnityEngine.Windows.Speech;
#endif

namespace UnitySpeechToText.Services
{
    /// <summary>
    /// Windows text transcription alternative.
    /// </summary>
    public class WindowsTextAlternative : TextAlternative
	{
		#if (UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN || UNITY_WINRT || UNITY_WINRT_8_0 || UNITY_WINRT_8_1 || UNITY_WINRT_10_0)
		/// <summary>
		/// Confidence level for the text transcription, either High, Medium, Low, or Rejected
		/// </summary>
		public ConfidenceLevel Confidence { get; set; }
		#endif
    }
}
