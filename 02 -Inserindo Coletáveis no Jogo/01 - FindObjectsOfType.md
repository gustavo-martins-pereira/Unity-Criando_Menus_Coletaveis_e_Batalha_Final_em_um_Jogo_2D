# FindObjectsOfType

Uma das ferramentas que os desenvolvedores usam para buscar objetos no jogo é o `FindObjectsOfType`, mas é aqui que precisamos ter cuidado para manter nossa performance otimizada. 

Afinal, ao desenvolver um jogo na Unity, é importante garantir que ele rode de maneira suave e rápida, não importa o quão complexo ou grande ele seja! Vamos entender melhor esse método e como utilizá-lo.

## Entendendo FindObjectsOfType

O método `FindObjectsOfType` é usado para encontrar todos os objetos de um determinado tipo em uma cena. Por exemplo, se você quiser encontrar todos os inimigos presentes, você pode usar esse método para fazer isso. Parece super útil, certo? E é, mas tem um porém.

Confira o seguinte código:

```csharp
Enemy[] enemies = FindObjectsOfType<Enemy>();
```

Este código busca todos os objetos do tipo `Enemy` na cena.

O problema com `FindObjectsOfType` é que ele é muito pesado em termos de performance, especialmente em jogos grandes com muitos objetos. Isso acontece porque a Unity precisa vasculhar todos os objetos na cena para encontrar aqueles que correspondem ao tipo que você está procurando. 

Se você usar esse método muitas vezes ou em cenas muito carregadas, seu jogo pode começar a apresentar lentidão.

## Alternativas e melhores práticas

Para manter a performance do seu jogo otimizada, pode seguir algumas recomendações:

### 1. Referências diretas

Sempre que possível, tente usar referências diretas. Isso significa que, em vez de buscar objetos quando você precisar deles, você mantém uma referência a eles desde o início. Isso pode ser feito arrastando e soltando o objeto no editor ou configurando as referências via código no momento da inicialização.

### 2. Uso de tags

Você pode marcar seus objetos com tags específicas e depois usar `GameObject.FindWithTag` para encontrar o primeiro objeto que corresponde à tag desejada. Isso ainda pode ser pesado, mas é geralmente mais eficiente do que usar `FindObjectsOfType` se você está procurando apenas um objeto.

### 3. Gerenciamento manual de listas

Uma das melhores práticas é gerenciar suas próprias listas de objetos. Por exemplo, você pode ter uma lista de todos os inimigos no jogo. 

Sempre que um inimigo é criado, você o adiciona à lista, e quando é destruído, você o remove. Isso permite que você acesse rapidamente todos os inimigos sem precisar buscar por eles. Como no exemplo a seguir:


```csharp
public class EnemyManager : MonoBehaviour
{
    public static List<Enemy> enemies = new List<Enemy>();

    public static void AddEnemy(Enemy enemy)
    {
        enemies.Add(enemy);
    }

    public static void RemoveEnemy(Enemy enemy)
    {
        enemies.Remove(enemy);
    }
}
```

Embora `FindObjectsOfType` possa ser uma ferramenta útil, é importante usá-la com cautela para evitar problemas de performance. Ao seguir as alternativas e melhores práticas mencionadas, você pode manter seu jogo rodando corretamente!