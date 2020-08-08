using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Profiling;

public class FPSCounter : MonoBehaviour
{
	[SerializeField]
	private float m_updateInterval = 0.1f;

	private float m_accum;
	private int m_frames;
	private float m_timeleft;
	private float m_fps;

	private float m = 0;

	private void Start()
	{
		Application.targetFrameRate = 30;
	}

	private void Update()
	{
		m_timeleft -= Time.deltaTime;
		m_accum += Time.timeScale / Time.deltaTime;
		m_frames++;

		if (0 < m_timeleft) return;

		m_fps = m_accum / m_frames;
		m_timeleft = m_updateInterval;
		m_accum = 0;
		m_frames = 0;


		m = (System.GC.GetTotalMemory(false) + Profiler.usedHeapSizeLong) / 1024f;
	}

	private void OnGUI()
	{
#if UNITY_EDITOR
		//GUILayout.Label("FPS: " + m_fps.ToString("f2") + "\n" + "MEMORY:" + m / 1024f);
#endif
	}
}