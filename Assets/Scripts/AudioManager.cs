using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

// Enumeração que define os diferentes tipos de efeitos sonoros (SFX) disponíveis no jogo.
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

// Enumeração que define os grupos de mixagem de áudio (MixerGroup).
// Esses grupos correspondem aos diferentes canais no mixer de áudio, como o volume geral, efeitos sonoros e sons do ambiente.
public enum MixerGroup
{
    Master,
    SFX,
    Environment
}

// Estrutura que representa uma configuração para um efeito sonoro específico.
// Contém o tipo do efeito, o clipe de áudio correspondente e a escala de volume para reprodução.
[Serializable]
struct SFXConfig
{
    public SFX Type;                // Tipo do efeito sonoro, definido na enumeração SFX.
    public AudioClip AudioClip;     // Arquivo de áudio que será reproduzido para este efeito.
    public float VolumeScale;       // Escala de volume para ajustar a intensidade do som.
}

// Classe responsável por gerenciar os efeitos sonoros e volumes no jogo.
// Ela controla a reprodução de efeitos sonoros e permite o ajuste de volumes usando o mixer de áudio.
public class AudioManager : MonoBehaviour
{
    // Referência ao AudioMixer usado para controlar os volumes no jogo.
    [SerializeField] AudioMixer AudioMixer;

    // Fonte de áudio usada para reproduzir efeitos sonoros (SFX).
    [SerializeField] private AudioSource SFXAudioSource;

    // Fonte de áudio usada para reproduzir sons do ambiente.
    [SerializeField] private AudioSource EnvironmentAudioSource;

    // Lista de configurações de efeitos sonoros, configurável no Editor da Unity.
    [SerializeField] private SFXConfig[] SFXConfigs;

    // Dicionário que mapeia cada tipo de efeito sonoro (SFX) para sua configuração correspondente.
    private Dictionary<SFX, SFXConfig> SFXs;

    // Dicionário que associa os grupos de mixagem (MixerGroup) aos nomes das variáveis de volume no mixer de áudio.
    private Dictionary<MixerGroup, string> MixerGroups;

    // Método chamado automaticamente pela Unity quando o objeto é inicializado.
    private void Awake()
    {
        // Inicializa o dicionário de grupos de mixagem com os nomes das variáveis correspondentes no mixer de áudio.
        MixerGroups = new()
        {
            { MixerGroup.Master, "MasterVolume" },
            { MixerGroup.SFX, "SFXVolume" },
            { MixerGroup.Environment, "EnvironmentVolume" }
        };

        // Converte a lista de configurações de SFX em um dicionário para acesso rápido às configurações.
        SFXs = SFXConfigs.ToDictionary(sfxConfig => sfxConfig.Type, sfxConfig => sfxConfig);
    }

    // Método público para reproduzir um efeito sonoro específico.
    // Recebe o tipo do efeito sonoro como parâmetro e reproduz o clipe de áudio associado.
    public void PlaySFX(SFX type)
    {
        // Verifica se o tipo de efeito sonoro está presente no dicionário de configurações.
        if (SFXs.ContainsKey(type))
        {
            // Recupera a configuração correspondente ao tipo de efeito sonoro.
            SFXConfig config = SFXs[type];

            // Usa a fonte de áudio para reproduzir o clipe com a escala de volume especificada.
            SFXAudioSource.PlayOneShot(config.AudioClip, config.VolumeScale);
        }
    }

    // Método público para ajustar o volume de um grupo de mixagem no mixer de áudio.
    // Recebe o grupo de mixagem e o valor normalizado (0 a 1) para ajustar o volume.
    public void SetMixerVolume(MixerGroup group, float normalizedValue)
    {
        // Recupera o nome da variável de volume correspondente ao grupo de mixagem.
        string groupString = MixerGroups[group];

        // Converte o valor normalizado em decibéis usando uma escala logarítmica.
        float volume = Mathf.Log10(normalizedValue) * 20;

        // Define o valor da variável de volume no mixer de áudio.
        AudioMixer.SetFloat(groupString, volume);
    }

    // Método público para obter o volume atual de um grupo de mixagem.
    // O volume pode ser retornado em formato normalizado (0 a 1) ou em decibéis.
    public float GetMixerVolume(MixerGroup group, bool normalize = true)
    {
        // Recupera o nome da variável de volume correspondente ao grupo de mixagem.
        string groupString = MixerGroups[group];

        // Obtém o valor atual da variável de volume no mixer de áudio.
        AudioMixer.GetFloat(groupString, out float volume);

        // Se o valor deve ser normalizado, converte de decibéis para a escala de 0 a 1.
        if (normalize)
        {
            volume = Mathf.Pow(10, volume / 20);
        }

        // Retorna o volume na escala apropriada.
        return volume;
    }
}
