using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    // Refer�ncia ao bot�o para voltar ao menu anterior.
    [SerializeField] private Button backButton;

    // Refer�ncia ao slider para controlar o volume master.
    [SerializeField] private Slider MasterVolumeSlider;

    // Refer�ncia ao slider para controlar o volume de efeitos sonoros (SFX).
    [SerializeField] private Slider SFXVolumeSlider;

    // Refer�ncia ao slider para controlar o volume do ambiente (background).
    [SerializeField] private Slider EnvironmentVolumeSlider;

    // M�todo chamado no in�cio da execu��o.
    private void Start()
    {
        // Adiciona um listener para o bot�o de voltar, que ir� chamar o m�todo ClosePanel ao ser clicado.
        backButton.onClick.AddListener(ClosePanel);

        // Adiciona listeners para cada slider. Sempre que o valor do slider mudar, o m�todo correspondente ser� chamado.
        MasterVolumeSlider.onValueChanged.AddListener(OnMasterVolumeSliderChanged);
        SFXVolumeSlider.onValueChanged.AddListener(OnSFXVolumeSliderChanged);
        EnvironmentVolumeSlider.onValueChanged.AddListener(OnEnvironmentVolumeSliderChanged);

        // Inicializa os valores dos sliders com os volumes atuais do AudioManager.
        // Usa SetValueWithoutNotify para definir os valores sem disparar o evento de mudan�a.
        MasterVolumeSlider.SetValueWithoutNotify(GameManager.Instance.AudioManager.GetMixerVolume(MixerGroup.Master));
        SFXVolumeSlider.SetValueWithoutNotify(GameManager.Instance.AudioManager.GetMixerVolume(MixerGroup.SFX));
        EnvironmentVolumeSlider.SetValueWithoutNotify(GameManager.Instance.AudioManager.GetMixerVolume(MixerGroup.Environment));
    }

    // M�todo respons�vel por fechar o painel de op��es quando o bot�o "Voltar" � pressionado.
    private void ClosePanel()
    {
        // Toca o som de clique quando o bot�o de voltar � pressionado.
        GameManager.Instance.AudioManager.PlaySFX(SFX.ButtonClick);

        // Desativa o painel de op��es, tornando-o invis�vel.
        gameObject.SetActive(false);
    }

    // M�todo chamado quando o valor do slider de volume master � alterado.
    private void OnMasterVolumeSliderChanged(float value)
    {
        // Atualiza o volume master no AudioManager com o novo valor do slider.
        GameManager.Instance.AudioManager.SetMixerVolume(MixerGroup.Master, value);
    }

    // M�todo chamado quando o valor do slider de volume de efeitos sonoros � alterado.
    private void OnSFXVolumeSliderChanged(float value)
    {
        // Atualiza o volume de efeitos sonoros (SFX) no AudioManager com o novo valor do slider.
        GameManager.Instance.AudioManager.SetMixerVolume(MixerGroup.SFX, value);
    }

    // M�todo chamado quando o valor do slider de volume do ambiente � alterado.
    private void OnEnvironmentVolumeSliderChanged(float value)
    {
        // Atualiza o volume do ambiente no AudioManager com o novo valor do slider.
        GameManager.Instance.AudioManager.SetMixerVolume(MixerGroup.Environment, value);
    }
}
