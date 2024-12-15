# Movimentação com Física

Na Unity, o código pode ser executado em diferentes momentos do ciclo de vida do jogo. Dois desses momentos são gerenciados pelas funções `Update` e `FixedUpdate`. Entender a diferença entre eles é crucial, especialmente quando você está lidando com cálculos de física que precisam ser precisos.

## O que é Update?

`Update` é uma função que é chamada uma vez por frame. Isso significa que, se o seu jogo estiver rodando a 60 quadros por segundo (fps), `Update` será chamado 60 vezes por segundo. 

A frequência com que `Update` é chamado pode variar, pois depende do desempenho do jogo e do hardware em que está sendo executado. Isso o torna ideal para lidar com a maioria das atualizações de lógica do jogo, como:

- Capturar entradas do usuário (teclado, mouse, controle);
- Atualizar a lógica de movimentação dos personagens;
- Executar verificações que não dependem de cálculos de física.

### Exemplo de uso do Update:

```csharp
void Update() {
    if (Input.GetKeyDown(KeyCode.Space)) {
        Debug.Log("Espaço foi pressionado!");
    }
}
```

Este código verifica, a cada frame, se o botão de espaço foi pressionado e, em caso afirmativo, imprime uma mensagem no console.

## O que é FixedUpdate?

`FixedUpdate`, por outro lado, é chamado em intervalos de tempo regulares. Por padrão, isso acontece a cada 0,02 segundos (50 vezes por segundo), independentemente do número de frames por segundo que seu jogo está rodando. 

Isso faz com que `FixedUpdate` seja a escolha perfeita para lidar com tudo relacionado à física, pois garante que os cálculos sejam consistentes, independentemente da taxa de frames. Isso é essencial para:

- Aplicar forças, velocidades e outras interações físicas;
- Fazer cálculos que precisam de precisão e consistência, como colisões;
- Evitar problemas de física que podem surgir devido a variações na taxa de frames.

### Exemplo de uso do FixedUpdate:

```csharp
void FixedUpdate() {
    if (Input.GetKey(KeyCode.UpArrow)) {
        rigidbody.AddForce(Vector3.up * 10);
    }
}
```

Este código aplica uma força para cima ao objeto sempre que a seta para cima é pressionada, fazendo com que ele se mova de maneira consistente, graças ao `FixedUpdate`.

## Por que essa diferença é importante?

A diferença entre `Update` e `FixedUpdate` é fundamental para a criação de jogos no Unity, especialmente aqueles que dependem de física. Usar `Update` para cálculos de física pode levar a resultados imprevisíveis, pois a taxa na qual `Update` é chamado pode variar. Por outro lado, usar `FixedUpdate` para capturar entradas do usuário pode fazer com que algumas entradas sejam perdidas, já que `FixedUpdate` não é chamado a cada frame.

Usar cada um deles corretamente garantirá que seu jogo funcione de maneira suave e previsível, proporcionando uma experiência melhor para os jogadores.