using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class TempoMachine : MonoBehaviour {
    public static TempoMachine instance;

    TempoState state;
    public string debugState;
    public Volume postProcess;
    public ColorAdjustments colorGrading;
    public float tempoChange = 5;
    public GameObject chuvaParticle, snowParticle;
    public int chanceChuva = 1, chanceNeve = 1;

    Color posTarget, fundoTarget;

    System.Action<TempoState> OnChanceState;

    void Awake(){
        instance = this;
    }

    void Start(){
        postProcess.profile.TryGet(out colorGrading);
        ChangeState(new DiaState(this));
    }

    void FixedUpdate(){
        state?.Execute(Time.deltaTime);

        colorGrading.colorFilter.value = Color.Lerp(colorGrading.colorFilter.value, posTarget, Time.deltaTime);
        Camera.main.backgroundColor = Color.Lerp(Camera.main.backgroundColor, fundoTarget, Time.deltaTime);
    }

    public void ChangeState(TempoState state){
        this.state?.Exit();
        this.state = state;
        this.state?.Enter();

        debugState = state.GetType().Name;
        // Debug.Log(debugState);

        OnChanceState?.Invoke(state);
    }

    public void AddListener(System.Action<TempoState> action){
        OnChanceState += action;
    }

    public void RemoveListener(System.Action<TempoState> action){
        OnChanceState -= action;
    }

    public void ChangeAmbient(Color luz, Color fundo) {
        posTarget = luz;
        fundoTarget = fundo;
    }

    public void RainParticle(bool active) {
        chuvaParticle.SetActive(active);
    }

    public void SnowParticle(bool active) {
        snowParticle.SetActive(active);
    }
}
