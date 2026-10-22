# Correção do projeto existente — 15/10/2026

Unity **2022.3.62f3**, mantida sem migração ou novos pacotes. A cena existente foi corrigida. As células dos três Tilemaps e os hashes dos 44 PNGs originais foram comparados com o checkpoint anterior e permaneceram iguais.

1. **Problema da câmera:** Orthographic Size 6,2 mostrava uma área maior que os bounds reais do mapa, de 18 × 10 unidades, expondo o exterior e o spawn inferior.
2. **Bordas pretas:** câmera centralizada em `(0, 0, -10)`, Orthographic Size **4,95**, viewport completo e Game View em **1920 × 1080**. O enquadramento corta uma pequena parte externa das paredes, mantendo a abertura inferior. Nenhuma barra de UI foi adicionada.
3. **Paredes:** os contornos físicos dos tiles não acompanhavam a borda visual interna. TilemapCollider2D, CompositeCollider2D e seu Rigidbody2D redundante foram removidos de Paredes. `ArenaColliders` contém cinco BoxCollider2D: Top, Left, Right, BottomLeft e BottomRight, com bordas internas em **x = ±8,4** e **y = ±4,4**, deixando o vão **x = -1,65 a 1,65** livre. Foram testados os quatro lados e os quatro cantos. O collider do Player foi reduzido para a região corporal/pés.
4. **Inimigos encontrados:** Skeleton, Spider e Big Worm 1. Foram usadas as variações 1 de Skeleton e Spider.
5. **Ondas:** onda 1 = **5 Skeletons**; onda 2 = **8 Spiders**; onda 3 = **12 Big Worms**. Cada onda possui seu prefab exclusivo.
6. **Player:** Idle, Run e Attack em **oito direções**, usando as três folhas fornecidas. Direction escolhe a linha da folha; Speed alterna Idle/Run; Attack dispara ao criar o projétil e retorna automaticamente ao estado adequado. Nenhum quadro foi redesenhado.
7. **Inimigos:** Skeleton e Spider usam suas sequências Idle, Walk, Attack e Death. Big Worm usa Idle e Death, além dos quadros visíveis finais de `appear-atk` para Attack. **Não existe Walk do Big Worm no conjunto fornecido:** Idle é compartilhada entre os estados Idle e Run para manter o corpo animado enquanto ele se desloca. Death termina antes da destruição e da liberação da próxima onda.
8. **Entering → Chasing:** ao nascer, cada inimigo caminha até EntranceWaypoint. Somente a menos de **0,15 unidade** do waypoint muda para Chasing e passa a seguir o Player. Todos os 25 inimigos foram observados atravessando esse fluxo durante o ciclo completo.
9. **Spawns:** `EnemySpawnRoot`, abaixo da arena. Spawn01 = **(-0,65, -6,5)**; Spawn02 = **(0, -6,5)**; Spawn03 = **(0,65, -6,5)**. O limite inferior da câmera é **-4,95**; inclusive os bounds completos dos renderers nascem fora da visão.
10. **Waypoint:** `EntranceWaypoint`, em **(0, -3,65)**, após a abertura, dentro da arena.
11. **Barreira:** `PlayerBarrier` em **(0, -4,525)**, BoxCollider2D de **3,3 × 0,25**, offset zero. Cobre só o vão. Player × PlayerBarrier ligado; Enemy × PlayerBarrier desligado.
12. **HUD:** Screen Space Overlay, Canvas Scaler 1920 × 1080 e Match 0,5. Margens de **48 px**, vida no topo esquerdo e pontuação/onda alinhadas à direita, com altura visual próxima de **120 px**. Preserve Aspect, textos centralizados nas aberturas e ajuste de fonte entre 20–24 para acomodar 2500 pontos. A vida usa Filled Horizontal, origem esquerda, atrás da moldura, limitada por RectMask2D à abertura interna.
13. **Scripts alterados:** PlayerController, PlayerHealth, EnemyController e WaveManager. GameManager, UIManager, MainMenu e Projectile foram preservados. Permanecem oito scripts de jogo; não foi criado sistema paralelo.
14. **Prefabs:** Player movido para `Prefabs/Player/Player.prefab`; Projectile movido para `Prefabs/Combat/Projectile.prefab`, preservando GUIDs. O Enemy existente virou `Prefabs/Enemies/Enemy_Wave1.prefab`; foram criados Enemy_Wave2 e Enemy_Wave3. Todos os personagens usam Transform Scale **1**. PPU: Player/Spider **32**, Skeleton/Big Worm **48**, mantendo tamanhos visuais coerentes. Point, sem compressão, nos sprites utilizados.
15. **Controllers:** `Animator/PlayerAnimator.controller`, `SkeletonAnimator.controller`, `SpiderAnimator.controller` e `WormAnimator.controller`. Foram criados **35 clips** usando somente os sprites já disponíveis.
16. **Ondas testadas:** contagens reais **5/8/12**, tipos exclusivos, spawns fora da câmera, entrada pela abertura e perseguição após o waypoint. A próxima onda aguarda todas as mortes e o intervalo de 2 s.
17. **Vitória:** ciclo automático completo **Menu → Jogar → ondas 1/2/3 → Vitória**, com valores normais de gameplay, pontuação **2500**, mortes animadas e Jogar Novamente restaurando vida 100, score 0 e onda 1. Os inimigos não foram imobilizados nesse ciclo.
18. **Game Over:** contato real com inimigos reduziu a vida a zero, exibiu Game Over e pontuação final. Jogar Novamente removeu a partida anterior e restaurou os valores iniciais.
19. **Console:** ao terminar o ciclo completo, **0 Errors e 0 Warnings**, incluindo ausência de erros de Animator. A auditoria final também verifica scripts e quadros de animação ausentes.

## Evidência e método

O registro completo está em `Docs/Validacao-Correcao.txt`. Foram feitas renderizações da Game View em 1920 × 1080 e inspeções visuais do enquadramento, HUD, paredes, três inimigos, ataques, Vitória e Game Over. O verificador controla movimento e mira pelas rotinas do Player e percorre as ações dos botões durante Play Mode; não depende de entrada física de teclado/mouse na janela. O teste de combate completo mantém HP, dano, velocidade e intervalos originais.

Antes das alterações, foi criado um checkpoint ZIP do estado existente. Cópia permanente local: `C:\Users\Davi\.codex\visualizations\2026\10\15\01a14069-e9ee-7a63-9215-58b11d85ddf5\Checkpoint-Arena-antes-correcao.zip`. Nenhum commit ou push foi feito. As exclusões anteriores de tiles feitas pelo autor foram preservadas.

As fontes/licenças dos assets não identificadas continuam a cargo do autor, conforme o README. Nenhum PNG original foi modificado e nenhum asset externo foi adicionado.
