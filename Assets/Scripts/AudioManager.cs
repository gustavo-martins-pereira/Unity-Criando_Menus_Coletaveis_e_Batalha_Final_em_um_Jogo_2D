using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

// Enumera��o que define os diferentes tipos de efeitos sonoros (SFX) dispon�veis no jogo.
// Cada valor representa um tipo de som que pode ser reproduzido, como passos, pulo ou ataques do jogador.
public enum SFX
{
    PlayerWalk,
    PlayerJump,
    PlayerAttack,
    PlayerHurt,
    PlayerDeath,
    ButtonClick,
}

// Enumera��o que define os grupos de mixagem de �udio (MixerGroup).
// Esses grupos correspondem aos diferentes canais no mixer de �udio, como o volume geral, efeitos sonoros e sons do ambiente.
public enum MixerGroup
{
    Master,
    SFX,
    Environment
}

// Estrutura que representa uma configura��o para um efeito sonoro espec�fico.
// Cont�m o tipo do efeito, o clipe de �udio correspondente e a escala de volume para reprodu��o.
[Serializable]
struct SFXConfig
{
    public SFX Type;                // Tipo do efeito sonoro, definido na enumera��o SFX.
    public AudioClip AudioClip;     // Arquivo de �udio que ser� reproduzido para este efeito.
    public float VolumeScale;       // Escala de volume para ajustar a intensidade do som.
}

// Classe respons�vel por gerenciar os efeitos sonoros e volumes no jogo.
// Ela controla a reprodu��o de efeitos sonoros e permite o ajuste de volumes usando o mixer de �udio.
public class AudioManager : MonoBehaviour
{
    // Refer�ncia ao AudioMixer usado para controlar os volumes no jogo.
    [SerializeField] AudioMixer AudioMixer;

    // Fonte de �udio usada para reproduzir efeitos sonoros (SFX).
    [SerializeField] private AudioSource SFXAudioSource;

    // Fonte de �udio usada para reproduzir sons do ambiente.
    [SerializeField] private AudioSource EnvironmentAudioSource;

    // Lista de configura��es de efeitos sonoros, configur�vel no Editor da Unity.
    [SerializeField] private SFXConfig[] SFXConfigs;

    // Dicion�rio que mapeia cada tipo de efeito sonoro (SFX) para sua configura��o correspondente.
    private Dictionary<SFX, SFXConfig> SFXs;

    // Dicion�rio que associa os grupos de mixagem (MixerGroup) aos nomes das vari�veis de volume no mixer de �udio.
    private Dictionary<MixerGroup, string> MixerGroups;

    // M�todo chamado automaticamente pela Unity quando o objeto � inicializado.
    private void Awake()
    {
        // Inicializa o dicion�rio de grupos de mixagem com os nomes das vari�veis correspondentes no mixer de �udio.
        MixerGroups = new()
        {
            { MixerGroup.Master, "MasterVolume" },
            { MixerGroup.SFX, "SFXVolume" },
            { MixerGroup.Environment, "EnvironmentVolume" }
        };

        // Converte a lista de configura��es de SFX em um dicion�rio para acesso r�pido �s configura��es.
        SFXs = SFXConfigs.ToDictionary(sfxConfig => sfxConfig.Type, sfxConfig => sfxConfig);
    }

    // M�todo p�blico para reproduzir um efeito sonoro espec�fico.
    // Recebe o tipo do efeito sonoro como par�metro e reproduz o clipe de �udio associado.
    public void PlaySFX(SFX type)
    {
        // Verifica se o tipo de efeito sonoro est� presente no dicion�rio de configura��es.
        if (SFXs.ContainsKey(type))
        {
            // Recupera a configura��o correspondente ao tipo de efeito sonoro.
            SFXConfig config = SFXs[type];

            // Usa a fonte de �udio para reproduzir o clipe com a escala de volume especificada.
            SFXAudioSource.PlayOneShot(config.AudioClip, config.VolumeScale);
        }
    }

    // M�todo p�blico para ajustar o volume de um grupo de mixagem no mixer de �udio.
    // Recebe o grupo de mixagem e o valor normalizado (0 a 1) para ajustar o volume.
    public void SetMixerVolume(MixerGroup group, float normalizedValue)
    {
        // Recupera o nome da vari�vel de volume correspondente ao grupo de mixagem.
        string groupString = MixerGroups[group];

        // Converte o valor normalizado em decib�is usando uma escala logar�tmica.
        float volume = Mathf.Log10(normalizedValue) * 20;

        // Define o valor da vari�vel de volume no mixer de �udio.
        AudioMixer.SetFloat(groupString, volume);
    }

    // M�todo p�blico para obter o volume atual de um grupo de mixagem.
    // O volume pode ser retornado em formato normalizado (0 a 1) ou em decib�is.
    public float GetMixerVolume(MixerGroup group, bool normalize = true)
    {
        // Recupera o nome da vari�vel de volume correspondente ao grupo de mixagem.
        string groupString = MixerGroups[group];

        // Obt�m o valor atual da vari�vel de volume no mixer de �udio.
        AudioMixer.GetFloat(groupString, out float volume);

        // Se o valor deve ser normalizado, converte de decib�is para a escala de 0 a 1.
        if (normalize)
        {
            volume = Mathf.Pow(10, volume / 20);
        }

        // Retorna o volume na escala apropriada.
        return volume;
    }
}
