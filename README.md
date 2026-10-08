# Rodinei: Longe de casa

Jogo de plataforma 2D desenvolvido em **Unity 6**. Rodinei partiu em busca de aventura, mas percebeu longe de casa que tinha esquecido o guarda-chuva. A jornada vira uma corrida contra o tempo: atravessar 4 fases cheias de obstáculos e recuperar o guarda-chuva no menor tempo possível.

**Versão final: v1.0** — Entrega Final (Jogo 2D Completo)

---

## Jogar

1. Baixe o arquivo `.zip` da build na página de [**Releases (v1.0)**](https://github.com/julionuness/project-umbrella/releases/tag/v1.0).
2. Extraia o `.zip` (mantenha o executável e a pasta `_Data` juntos).
3. Execute o `.exe` do jogo (Windows).

### Controles

| Ação | Tecla |
|---|---|
| Andar para a esquerda / direita | `A` / `D` |
| Pular | `Espaço` |
| Pulo duplo | `Espaço` (de novo, no ar) |
| Menus e painéis | Mouse |

### Como o jogo funciona

- Avance sempre para a direita até o fim de cada fase, desviando dos obstáculos.
- **Não há checkpoints:** qualquer derrota (obstáculo ou queda no limbo) reinicia a **fase atual** do início.
- Um **cronômetro** acompanha toda a partida; a meta é terminar no menor tempo.
- Na **Fase 4**, pegue o guarda-chuva para vencer.

| Fase | Foco |
|---|---|
| 1 — Tutorial | Controles na tela e uma armadilha de demonstração |
| 2 — Desafios progressivos | Vãos no terreno, espinhos, flechas, bigornas, pêndulos e barris |
| 3 — Domínio de habilidade | Obstáculos combinados, serras em trajeto e trechos de descida com reflexo |
| 4 — Mapa final | Trecho curto até o guarda-chuva |

---

## Abrir o projeto no Unity

**Requisitos:** Unity **6000.3.22f1** (Unity 6.3 LTS) e **Git LFS**.

> Instale o Git LFS **antes** de clonar (`git lfs install`). Imagens, áudios e fontes são versionados com LFS; sem ele, esses arquivos chegam como pequenos ponteiros de texto e o projeto abre com recursos faltando.

```bash
git lfs install
git clone https://github.com/julionuness/project-umbrella.git
```

1. Abra a pasta no **Unity Hub** com a versão 6000.3.22f1.
2. Abra a cena `Assets/Scenes/Menu.unity` e aperte **Play**.
3. Ordem das cenas no Build Settings: `Menu` → `Fase_01` → `Fase_02` → `Fase_03` → `Fase_04`.

O projeto usa a API de entrada `UnityEngine.Input`; em **Project Settings > Player > Active Input Handling** a opção deve ficar em **Both** (já configurada).

---

## Estrutura do repositório

```
├── Assets/
│   ├── Animations/     # Animator do jogador e clipes (Idle, Walk, Jump)
│   ├── Art/            # Sprites, fundos e pacotes de arte
│   ├── Audio/          # Música e efeitos sonoros
│   ├── Prefabs/        # 26 prefabs (jogador, obstáculos, terreno, interface)
│   ├── Resources/      # GameTimer.prefab (carregado automaticamente)
│   ├── Scenes/         # Menu e Fase_01 a Fase_04
│   ├── Scripts/        # Código-fonte em C#
│   └── Tilesets/       # Paleta de terreno
├── Packages/           # Dependências do projeto (manifest.json)
├── ProjectSettings/    # Configurações do Unity
├── Docs/
│   ├── GDD/            # GDD final (Word)
│   ├── LevelDesign/    # Documento de level design e croquis das fases
│   ├── Testes/         # Fichas de playtest e lista de bugs
│   └── Arte/           # Model sheet, sprite sheet, VFX e tela inicial
├── GDD.pdf             # GDD final (v1.0)
├── CREDITS.md          # Créditos dos recursos externos
└── README.md
```

---

## Código-fonte (`Assets/Scripts`)

| Script | Responsabilidade |
|---|---|
| `PlayerMovement` | Movimento lateral, pulo e pulo duplo, giro do sprite, parâmetros do Animator e som de passos |
| `CameraFollow` | Câmera que acompanha o jogador suavemente |
| `ParallaxLayer` | Camadas de fundo com profundidade (parallax) |
| `SceneTransitionManager` | Fade entre cenas (persistente) |
| `MainMenu` | Iniciar o jogo (zera o cronômetro) e sair |
| `GameTimer` | Cronômetro geral da partida e HUD (persistente) |
| `GameOverManager` | Derrota: painel "Você perdeu!" e reinício da fase |
| `VictoryManager` | Vitória: painel, tempo total e navegação entre fases |
| `PhaseEndTrigger` | Zona invisível que encerra as fases 1 a 3 |
| `UmbrellaPickup` | Guarda-chuva (gira e flutua) que encerra o jogo na Fase 4 |
| `LimboZone` | Derrota por queda fora do mapa |
| `DeathObstacles` | Obstáculo letal por contato (ex.: espinhos) |
| `RotatingSaw` | Serra giratória em vaivém entre dois pontos |
| `PendulumObstacle` | Obstáculo pendular em torno de um pivô |
| `AnvilTrap` | Bigorna que cai quando o jogador se aproxima e vira chão depois de pousar |
| `ArrowTrap` | Flecha que mira e dispara em loop contra um alvo |
| `BarrelSpawner` / `Barrel` | Gerador de barris que rolam ladeira abaixo com física real |
| `MovingPlatform` | Plataforma cinemática entre dois pontos |
| `AudioManager` | Música de fundo e efeitos sonoros (persistente) |
| `RainEffect` / `SunRaysEffect` / `ConfettiEffect` | Efeitos de interface criados por código (derrota e vitória) |

---

## Documentação

| Documento | Local |
|---|---|
| GDD final | [`GDD.pdf`](GDD.pdf) (versão editável em [`Docs/GDD`](Docs/GDD)) |
| Level design e croquis das fases | [`Docs/LevelDesign`](Docs/LevelDesign) |
| Registros de testes (playtests e bugs) | [`Docs/Testes`](Docs/Testes) |
| Créditos dos recursos externos | [`CREDITS.md`](CREDITS.md) |

## Versões

| Tag | Entrega |
|---|---|
| `v0.1` | GDD inicial |
| `v0.2` | Direção visual e level design |
| `v0.3` | Prova de conceito jogável |
| `v0.4` | Vertical slice |
| `v0.5` | Beta / feature lock |
| `v1.0` | **Entrega final** |

---

## Equipe

| Integrante | Papel |
|---|---|
| Júlio César Nunes | Game Designer + Producer; Artista 2D + Animador |
| Thales Silveira de Queiroz | Level Designer + Sound Designer |
| Gustavo Vitor Ferreira | Programador + QA; Artista 2D + Animador |
