# StateMachineBehavior

Imagine um personagem em um jogo 2D, correndo, pulando e atacando inimigos. Cada ação corresponde a um estado de animação, e gerenciar a transição entre esses estados de forma fluida e precisa é crucial. É nesse momento que os State Machine Behaviours brilham!

**State Machine Behaviours (SMBs)** são scripts que residem dentro de um Animator na Unity. Eles permitem que você adicione lógica personalizada a estados específicos de uma máquina de estado, expandindo suas funcionalidades e flexibilidade.

Com SMBs, você pode:

* **Executar código quando um estado é iniciado, atualizado ou finalizado:** isso permite que você execute ações específicas em momentos chave da animação, como reproduzir sons, aplicar efeitos visuais ou modificar propriedades do objeto.
* **Monitorar transições de estado:** você pode detectar quando um estado muda e executar código em resposta a essa mudança, criando transições suaves e lógicas entre animações.
* **Controlar o comportamento do Animator:** SMBs permitem que você modifique o comportamento do Animator em tempo real, como ajustar a velocidade de reprodução da animação ou alterar a animação em execução com base em condições específicas.

### Vantagens 

Usar SMBs em suas animações no Unity oferece diversas vantagens:

* **Organização e modularidade:** SMBs promovem uma organização mais clara e modular do código de animação, separando a lógica específica de cada estado.
* **Reutilização e flexibilidade:** você pode criar SMBs reutilizáveis para diferentes estados ou animações, aumentando a flexibilidade e reduzindo a duplicação de código.
* **Controle preciso e personalização:** SMBs permitem um controle mais preciso sobre o comportamento da animação, abrindo portas para personalizações avançadas e efeitos criativos.

### Exemplos práticos 

Para ilustrar o poder dos SMBs, imagine alguns exemplos práticos:

* **Personagem andando e correndo:** um SMB pode ser usado para detectar quando o personagem muda de andar para correr, ajustando a velocidade de reprodução da animação e reproduzindo o som de passos mais rápidos.
* **Ataque de personagem:** ao atacar, um SMB pode aplicar efeitos visuais como flashes ou explosões, além de reproduzir sons de impacto e atualizar a vida dos inimigos.
* **Interação com objetos:** um SMB pode detectar colisões com objetos, alterando a animação do personagem para interagir com o objeto, como pegar um item ou abrir uma porta.


Para usar SMBs em seu projeto Unity, siga estes passos:

1. Crie um script C# que herde da classe `StateMachineBehaviour`.
2. Implemente os métodos desejados, como `OnStateEnter`, `OnStateUpdate` e `OnStateExit`, para executar código em diferentes momentos do estado.
3. No Animator do objeto animado, adicione o script SMB criado e selecione o estado ao qual ele deve ser aplicado.

>Lembre-se: os SMBs são apenas uma ferramenta, a verdadeira magia reside na sua visão e na forma como você os utiliza para dar vida às suas animações e encantar seus jogadores.