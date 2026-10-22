# Arena Survivor Project

**Estudante:** Davi Garcia  
**Unity:** 2022.3.62f3

Protótipo 2D de sobrevivência em uma arena. Derrote três ondas de inimigos para vencer; a partida termina em Game Over quando a vida chega a zero.

## Como jogar

Abra este projeto com Unity **2022.3.62f3**, abra `Assets/Projeto/Scenes/Menu.unity` e pressione Play. Clique em **Jogar**.

- WASD / setas: movimentação.
- Mouse: direção do tiro.
- Botão esquerdo do mouse: atirar (pode manter pressionado).
- Jogar Novamente: reinicia Gameplay, com vida 100, pontuação 0 e onda 1.
- Sair: fecha a versão compilada; no Editor registra uma mensagem no Console.

## Funcionalidades

- Movimento com Rigidbody2D e colisão com paredes e barreira da entrada.
- Vida inicial de 100 e barra funcional atrás da moldura fornecida.
- Ataque à distância com intervalo de 0,3 s; projétil com velocidade 10 e dano 1.
- Um tipo diferente por onda: Skeleton, Spider e Big Worm, nessa ordem. Todos têm vida 3, velocidade 2 e dano de contato 20, limitado a uma vez por segundo no jogador.
- Entrada dos inimigos pela abertura inferior, ignorando PlayerBarrier.
- Exatamente três ondas: 5, 8 e 12 inimigos. A próxima aguarda todos os spawns, todas as derrotas e intervalo de 2 s.
- Cada derrota concede 100 pontos; vitória completa totaliza 2500 pontos.
- Menu, HUD, Vitória e Game Over com pontuação final e reinício.
- Canvas com resolução de referência 1920 × 1080.
- Player com Idle, Run e Attack em oito direções; três inimigos com animações de locomoção, ataque e morte.
- Câmera ortográfica ajustada à arena em 1920 × 1080, cinco colliders de parede independentes e HUD ancorada à tela.
- Prefabs Player, Enemy_Wave1/2/3 e Projectile e scripts separados por responsabilidade.

Os parâmetros de velocidade, vida, dano e intervalos estão expostos no Inspector dos respectivos prefabs/componentes. As quantidades de ondas estão fixadas em `WaveManager.cs` para manter exatamente 5, 8 e 12.

## Organização

- `Assets/Projeto/Scenes`: Menu e Gameplay.
- `Assets/Projeto/Prefabs/Player`: Player.
- `Assets/Projeto/Prefabs/Enemies`: Enemy_Wave1, Enemy_Wave2 e Enemy_Wave3.
- `Assets/Projeto/Prefabs/Combat`: Projectile.
- `Assets/Projeto/Animator`: quatro Animator Controllers e 35 clips.
- `Assets/Projeto/Scripts`: Player, Enemy, Combat, Managers e UI.
- `Assets/Projeto/Sprites`: assets visuais fornecidos pelo autor.

Layers: Player (8), Enemy (9), Projectile (10), Walls (11) e PlayerBarrier (3). PlayerBarrier colide somente com Player; Projectile interage com Enemy e Walls como trigger.

## Assets

- Mapa/Tilemap e `Tileset-Terrain.png`: já presentes no projeto do autor.
- Personagem: folhas Idle, Run e Attack fornecidas pelo autor.
- Inimigos: Skeleton variation 1, Spider variation 1 e Big Worm 1, nas pastas fornecidas pelo autor. O Big Worm não contém Walk; sua sequência Idle é reutilizada durante a locomoção. O ataque usa os quadros visíveis finais de `appear-atk`, e a morte usa a sequência Death.
- Interface: UI_GameOver, UI_Vitoria, BTN_Jogar, BTN_Sair, BTN_JogarNovamente, HUD_Health, HUD_Score e HUD_Wave.
- Projétil: Projectile.png fornecido pelo autor.
- Textos: TextMeshPro e recursos essenciais Liberation Sans distribuídos com o pacote já instalado; avisos de licença em `Assets/TextMesh Pro`.

Para os assets visuais cuja origem não foi identificada: **Fonte/licença deste asset deve ser preenchida pelo autor.** Nenhum asset visual foi gerado ou buscado na internet; os PNGs originais foram preservados.

## Validação e pendências

Validação automatizada em Play Mode concluída na Unity 2022.3.62f3: Menu → Gameplay → três ondas → Vitória com 2500; Game Over por dano real de contato; reinício após ambos os resultados. O teste completo usou HP, dano, velocidades e intervalos normais, com movimentação e mira comandadas automaticamente pelos mesmos métodos usados pelos controles. Os inimigos permaneceram se movendo normalmente durante esse ciclo.

Também foram verificados: quatro paredes, quatro cantos, PlayerBarrier, oito direções e estados do Player, animações dos três inimigos e a passagem dos 25 spawns fora da câmera pelo waypoint antes da perseguição. As telas e os personagens foram renderizados e conferidos visualmente em 1920 × 1080. Resultado final: **0 Errors e 0 Warnings**.

Veja [Relatorio-Correcao.md](Relatorio-Correcao.md) e [Docs/Validacao-Correcao.txt](Docs/Validacao-Correcao.txt). O teste usa chamadas de controle dentro do Play Mode, sem depender de teclas/cliques físicos na janela. O fechamento do executável por Sair não faz parte deste passe; o botão continua usando Application.Quit na versão compilada.

As ferramentas temporárias de montagem e teste foram removidas para manter apenas os oito scripts do jogo. O mapa e os 44 PNGs foram preservados, incluindo as exclusões de tiles feitas previamente pelo autor. Foi salvo um checkpoint ZIP local antes das correções; nenhum commit ou push foi feito neste passe.
