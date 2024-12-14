using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    // Referência ao botão para voltar ao menu anterior.
    [SerializeField] private Button backButton;

    // Referência ao slider para controlar o volume master.
    [SerializeField] private Slider MasterVolumeSlider;

    // Referência ao slider para controlar o volume de efeitos sonoros (SFX).
    [SerializeField] private Slider SFXVolumeSlider;

    // Referência ao slider para controlar o volume do ambiente (background).
    [SerializeField] private Slider EnvironmentVolumeSlider;

    // Método chamado no início da execução.
    private void Start()
    {
        // Adiciona um listener para o botão de voltar, que irá chamar o método ClosePanel ao ser clicado.
        backButton.onClick.AddListener(ClosePanel);

        // Adiciona listeners para cada slider. Sempre que o valor do slider mudar, o método correspondente será chamado.
        MasterVolumeSlider.onValueChanged.AddListener(OnMasterVolumeSliderChanged);
        SFXVolumeSlider.onValueChanged.AddListener(OnSFXVolumeSliderChanged);
        EnvironmentVolumeSlider.onValueChanged.AddListener(OnEnvironmentVolumeSliderChanged);

        // Inicializa os valores dos sliders com os volumes atuais do AudioManager.
        // Usa SetValueWithoutNotify para definir os valores sem disparar o evento de mudança.
        MasterVolumeSlider.SetValueWithoutNotify(GameManager.Instance.AudioManager.GetMixerVolume(MixerGroup.Master));
        SFXVolumeSlider.SetValueWithoutNotify(GameManager.Instance.AudioManager.GetMixerVolume(MixerGroup.SFX));
        EnvironmentVolumeSlider.SetValueWithoutNotify(GameManager.Instance.AudioManager.GetMixerVolume(MixerGroup.Environment));
    }

    // Método responsável por fechar o painel de opções quando o botão "Voltar" é pressionado.
    private void ClosePanel()
    {
        // Toca o som de clique quando o botão de voltar é pressionado.
        GameManager.Instance.AudioManager.PlaySFX(SFX.ButtonClick);

        // Desativa o painel de opções, tornando-o invisível.
        gameObject.SetActive(false);
    }

    // Método chamado quando o valor do slider de volume master é alterado.
    private void OnMasterVolumeSliderChanged(float value)
    {
        // Atualiza o volume master no AudioManager com o novo valor do slider.
        GameManager.Instance.AudioManager.SetMixerVolume(MixerGroup.Master, value);
    }

    // Método chamado quando o valor do slider de volume de efeitos sonoros é alterado.
    private void OnSFXVolumeSliderChanged(float value)
    {
        // Atualiza o volume de efeitos sonoros (SFX) no AudioManager com o novo valor do slider.
        GameManager.Instance.AudioManager.SetMixerVolume(MixerGroup.SFX, value);
    }

    // Método chamado quando o valor do slider de volume do ambiente é alterado.
    private void OnEnvironmentVolumeSliderChanged(float value)
    {
        // Atualiza o volume do ambiente no AudioManager com o novo valor do slider.
        GameManager.Instance.AudioManager.SetMixerVolume(MixerGroup.Environment, value);
    }
}
