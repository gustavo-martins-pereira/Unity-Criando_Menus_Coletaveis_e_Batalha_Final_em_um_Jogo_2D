using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Enumeração que define os diferentes tipos de efeitos sonoros (SFX) disponíveis.
public enum SFX
{
    PlayerWalk,
    PlayerJump,
    PlayerAttack,
    PlayerHurt,
    PlayerDeath
}

// Estrutura para configurar os efeitos sonoros.
// Cada configuração inclui o tipo do efeito, o clipe de áudio e o volume.
[Serializable]
struct SFXConfig
{
    public SFX Type;
    public AudioClip AudioClip;
    public float VolumeScale;
}

// Classe responsável por gerenciar os efeitos sonoros do jogo.
public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource SFXAudioSource;
    [SerializeField] private AudioSource EnvironmentAudioSource;

    // Lista de configurações de efeitos sonoros configuráveis no Editor.
    [SerializeField] private SFXConfig[] SFXConfigs;

    // Dicionário que armazena as configurações de efeitos sonoros por tipo.
    private Dictionary<SFX, SFXConfig> SFXs;

    // Método chamado automaticamente pela Unity durante a inicialização.
    private void Awake()
    {
        // Converte a lista de configurações em um dicionário para acesso rápido.
        SFXs = SFXConfigs.ToDictionary(sfxConfig => sfxConfig.Type, sfxConfig => sfxConfig);
    }

    // Método público para reproduzir um efeito sonoro específico.
    public void PlaySFX(SFX type)
    {
        // Verifica se o tipo de efeito sonoro existe no dicionário.
        if (SFXs.ContainsKey(type))
        {
            // Recupera a configuração do efeito sonoro e o reproduz.
            SFXConfig config = SFXs[type];
            SFXAudioSource.PlayOneShot(config.AudioClip, config.VolumeScale);
        }
    }
}
