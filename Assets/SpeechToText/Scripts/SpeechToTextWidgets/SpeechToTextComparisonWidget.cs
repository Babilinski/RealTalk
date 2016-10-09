using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnitySpeechToText.Utilities;
using System.Collections;
using UnityEngine.Events;

namespace UnitySpeechToText.Widgets
{
    /// <summary>
    /// Widget that handles the side-by-side comparison of different speech-to-text services.
    /// </summary>
    public class SpeechToTextComparisonWidget : MonoBehaviour
    {

		public float requiredAccuracy = 30;
		public float waitTime = 3;

		public UnityEvent onStart;
		public UnityEvent onEnd;
		public UnityEvent onCorrect;
		public UnityEvent onWrong;


		public bool wasTriggered;

        /// <summary>
        /// Store for ResponsesTimeoutInSeconds property
        /// </summary>
        float m_ResponsesTimeoutInSeconds = 8f;
        /// <summary>
        /// Store for SpeechToTextServiceWidgets property
        /// </summary>
	
        SpeechToTextServiceWidget m_SpeechToTextServiceWidgets;
 
        /// <summary>
        /// Whether the application is currently in a speech-to-text session
        /// </summary>
        bool m_IsCurrentlyInSpeechToTextSession;
        /// <summary>
        /// Whether the application is currently recording audio
        /// </summary>
        bool m_IsRecording;
        /// <summary>
        /// Set of speech-to-text service widgets that are still waiting on a final response
        /// </summary>
        HashSet<SpeechToTextServiceWidget> m_WaitingSpeechToTextServiceWidgets = new HashSet<SpeechToTextServiceWidget>();

		public string[] CheckedPhrase;

        /// <summary>
        /// Number of seconds to wait for all responses after recording
        /// </summary>
        public float ResponsesTimeoutInSeconds { set { m_ResponsesTimeoutInSeconds = value; } }

        /// <summary>
        /// Array of speech-to-text service widgets 
        /// </summary>
        public SpeechToTextServiceWidget SpeechToTextServiceWidgets
        {
            set
            {
                m_SpeechToTextServiceWidgets = value;
                RegisterSpeechToTextServiceWidgetsCallbacks();
            }
		
        }

     

        /// <summary>
        /// Initialization function called on the frame when the script is enabled just before any of the Update
        /// methods is called the first time.
        /// </summary>
		void Start()
        {
			m_SpeechToTextServiceWidgets = Object.FindObjectOfType (typeof(SpeechToTextServiceWidget)) as SpeechToTextServiceWidget;
            RegisterSpeechToTextServiceWidgetsCallbacks();
			m_SpeechToTextServiceWidgets.OnResult.AddListener (() => GotResults ());

        }

		public void StartPhrase(){
			onStart.Invoke ();
			StartCoroutine (StartRecordingTimer ());

		}



		IEnumerator StartRecordingTimer(){
			wasTriggered = true;
			print ("Start");
			OnRecordButtonClicked ();
			yield return new WaitForSeconds (waitTime);
			OnRecordButtonClicked ();

		}

		public void GotResults(){
			if (wasTriggered != true)
				return;

			bool wasRight = false;

			foreach (float f in m_SpeechToTextServiceWidgets.speechAccuracy) {
				print (f);
				if (f > requiredAccuracy) {
					wasRight = true;
					onEnd.Invoke ();
					onCorrect.Invoke ();
					m_SpeechToTextServiceWidgets.speechAccuracy.Clear ();
					break;
				}
			}

			onEnd.Invoke ();
			onWrong.Invoke ();
			m_SpeechToTextServiceWidgets.speechAccuracy.Clear ();
			wasTriggered = false;
		}

        /// <summary>
        /// Function that is called when the MonoBehaviour will be destroyed.
        /// </summary>
        void OnDestroy()
        {
            UnregisterSpeechToTextServiceWidgetsCallbacks();
        }
			

        /// <summary>
        /// Registers callbacks with each SpeechToTextServiceWidget.
        /// </summary>
        void RegisterSpeechToTextServiceWidgetsCallbacks()
        {
            if (m_SpeechToTextServiceWidgets != null)
            {
                SmartLogger.Log(DebugFlags.SpeechToTextWidgets, "register service widgets callbacks");
				m_SpeechToTextServiceWidgets.RegisterOnRecordingTimeout(OnRecordTimeout);
				m_SpeechToTextServiceWidgets.RegisterOnReceivedLastResponse(OnSpeechToTextReceivedLastResponse);
             
            }
        }

        /// <summary>
        /// Unregisters callbacks with each SpeechToTextServiceWidget.
        /// </summary>
        void UnregisterSpeechToTextServiceWidgetsCallbacks()
        {
            if (m_SpeechToTextServiceWidgets != null)
            {
                SmartLogger.Log(DebugFlags.SpeechToTextWidgets, "unregister service widgets callbacks");
				m_SpeechToTextServiceWidgets.RegisterOnRecordingTimeout(OnRecordTimeout);
				m_SpeechToTextServiceWidgets.RegisterOnReceivedLastResponse(OnSpeechToTextReceivedLastResponse);

            }
        }




        /// <summary>
        /// Function that is called when the record button is clicked.
        /// </summary>
        public void OnRecordButtonClicked()
        {
            if (m_IsRecording)
            {
                StopRecording();
            }
            else
            {
                StartRecording();
            }
        }

        /// <summary>
        /// Function that is called when audio recording times out.
        /// </summary>
        void OnRecordTimeout()
        {
            StopRecording();
        }

        /// <summary>
        /// Function that is called when the given SpeechToTextServiceWidget has gotten its last response. If there are no waiting
        /// SpeechToTextServiceWidgets left, then this function will wrap-up the current comparison session.
        /// </summary>
        /// <param name="serviceWidget">The speech-to-text service widget that received a last response</param>
        void OnSpeechToTextReceivedLastResponse(SpeechToTextServiceWidget serviceWidget)
        {
            SmartLogger.Log(DebugFlags.SpeechToTextWidgets, "Response from " + serviceWidget.SpeechToTextServiceString());
            m_WaitingSpeechToTextServiceWidgets.Remove(serviceWidget);
            if (m_WaitingSpeechToTextServiceWidgets.Count == 0)
            {
                SmartLogger.Log(DebugFlags.SpeechToTextWidgets, "Responses from everyone");
                FinishComparisonSession();
            }
        }

        /// <summary>
        /// Starts recording audio for each speech-to-text service widget if not already recording.
        /// </summary>
        void StartRecording()
        {
            if (!m_IsRecording)
            {
                SmartLogger.Log(DebugFlags.SpeechToTextWidgets, "Start comparison recording");
                m_IsCurrentlyInSpeechToTextSession = true;
                m_IsRecording = true;
           
                m_WaitingSpeechToTextServiceWidgets.Clear();
               
                    SmartLogger.Log(DebugFlags.SpeechToTextWidgets, "tell service widget to start recording");
					m_SpeechToTextServiceWidgets.StartRecording();
                  
                
            }
        }

        /// <summary>
        /// Stops recording audio for each speech-to-text service widget if already recording. Also schedules a wrap-up of the
        /// current comparison session to happen after the responses timeout.
        /// </summary>
        void StopRecording()
        {
            if (m_IsRecording)
            {
                m_IsRecording = false;

               
                Invoke("FinishComparisonSession", m_ResponsesTimeoutInSeconds);

                // If a phrase is selected, pass it to the SpeechToTextServiceWidget.
				string[] comparisonPhrase = CheckedPhrase;
				m_SpeechToTextServiceWidgets.StopRecording (comparisonPhrase);
              
            }
        }

        /// <summary>
        /// Wraps up the current speech-to-text comparison session by enabling all UI interaction.
        /// </summary>
        void FinishComparisonSession()
        {
            // If this function is called before the timeout, cancel all invokes so that it is not called again upon timeout.
            CancelInvoke();

            if (m_IsCurrentlyInSpeechToTextSession)
            {
                m_IsCurrentlyInSpeechToTextSession = false;
            }
        }
			
    }
}
