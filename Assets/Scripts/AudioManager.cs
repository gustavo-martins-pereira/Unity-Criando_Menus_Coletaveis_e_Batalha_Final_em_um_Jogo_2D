using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Enumera��o que define os diferentes tipos de efeitos sonoros (SFX) dispon�veis.
public enum SFX
{
    PlayerWalk,
    PlayerJump,
    PlayerAttack,
    PlayerHurt,
    PlayerDeath
}

// Estrutura para configurar os efeitos sonoros.
// Cada configura��o inclui o tipo do efeito, o clipe de �udio e o volume.
[Serializable]
struct SFXConfig
{
    public SFX Type;
    public AudioClip AudioClip;
    public float VolumeScale;
}

// Classe respons�vel por gerenciar os efeitos sonoros do jogo.
public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource SFXAudioSource;
    [SerializeField] private AudioSource EnvironmentAudioSource;

    // Lista de configura��es de efeitos sonoros configur�veis no Editor.
    [SerializeField] private SFXConfig[] SFXConfigs;

    // Dicion�rio que armazena as configura��es de efeitos sonoros por tipo.
    private Dictionary<SFX, SFXConfig> SFXs;

    // M�todo chamado automaticamente pela Unity durante a inicializa��o.
    private void Awake()
    {
        // Converte a lista de configura��es em um dicion�rio para acesso r�pido.
        SFXs = SFXConfigs.ToDictionary(sfxConfig => sfxConfig.Type, sfxConfig => sfxConfig);
    }

    // M�todo p�blico para reproduzir um efeito sonoro espec�fico.
    public void PlaySFX(SFX type)
    {
        // Verifica se o tipo de efeito sonoro existe no dicion�rio.
        if (SFXs.ContainsKey(type))
        {
            // Recupera a configura��o do efeito sonoro e o reproduz.
            SFXConfig config = SFXs[type];
            SFXAudioSource.PlayOneShot(config.AudioClip, config.VolumeScale);
        }
    }
}
