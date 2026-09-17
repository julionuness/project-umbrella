# Rodinei: Longe de casa

Jogo de plataforma 2D desenvolvido em Unity 6 (URP 2D). Rodinei está voltando pra casa em busca do
guarda-chuva que esqueceu — o objetivo é atravessar a fase, desviando dos obstáculos, e coletar o
guarda-chuva no final.

## Status atual — Entrega 3 (Prova de Conceito Jogável)

- [x] Controles funcionando (andar, pular, pulo duplo)
- [x] Mecânica principal implementada (Double Jump)
- [x] Core loop jogável (Menu → Fase → Vitória/Derrota → Reinício)
- [x] Física e colisões (Rigidbody2D/Collider2D, Tilemap + Composite Collider)
- [x] Interações básicas (obstáculos e coletável)
- [x] Objetivo identificável (guarda-chuva)
- [x] Condição básica de vitória/derrota
- [x] Primeiros prefabs
- [x] GDD atualizado (`GDD.pdf`, v0.3)
- [ ] Tag `v0.3` no repositório (a confirmar após o push)

## Controles

| Ação | Tecla |
|---|---|
| Andar para a esquerda/direita | `A` / `D` ou setas |
| Pular | `Espaço` |
| Pulo duplo | `Espaço` (2ª vez, no ar) |

## Como abrir o projeto

1. Clone o repositório.
2. Abra a pasta do projeto no **Unity Hub** (Unity 6.3 LTS ou compatível).
3. Certifique-se de que o pacote de **Input System** está instalado, e que
   **Edit > Project Settings > Player > Active Input Handling** está em `Both` (o projeto usa a API
   legada `UnityEngine.Input`).
4. Abra a cena em `Assets/Scenes/SampleScene.unity` e dê Play.

## Estrutura de scripts (`Assets/Scripts`)

| Script | Responsabilidade |
|---|---|
| `PlayerMovement.cs` | Movimento lateral, pulo e double jump, flip de sprite, integração com o Animator |
| `CameraFollow.cs` | Câmera acompanhando o player suavemente |
| `GameOverManager.cs` | Painel de derrota, pausa do jogo, reinício |
| `VictoryManager.cs` | Painel de vitória, tempo total, reinício |
| `LimboZone.cs` | Game Over ao cair fora do cenário |
| `RotatingSaw.cs` | Serra que gira e se move em vaivém entre dois pontos |
| `PendulumObstacle.cs` | Obstáculo pendular em torno de um ponto fixo |
| `AnvilTrap.cs` | Bigorna que detecta o player, cai com física real e vira chão após pousar |
| `ArrowTrap.cs` | Flecha que mira e dispara em loop contra um alvo |
| `Barrel.cs` / `BarrelSpawner.cs` | Gerador de barris que rolam ladeira abaixo com física real |
| `MovingPlatform.cs` | Plataforma cinemática entre dois pontos |
| `UmbrellaPickup.cs` | Coletável que gira/flutua e dispara a vitória |
| `ParallaxLayer.cs` | Camadas de fundo com profundidade (parallax) |

## Documentação

- **GDD completo**: `GDD.pdf` (Game Design Document, versão 0.3)
- **Tags de entrega**: [`v0.1`](../../releases/tag/v0.1) · [`v0.2`](../../releases/tag/v0.2) · `v0.3`

## Equipe

| Integrante | Papel |
|---|---|
| Júlio César Nunes | Game Designer + Producer |
| Thales Silveira de Queiroz | Level Designer + Sound Designer |
| Gustavo Vitor Ferreira | Programador + QA, Artista 2D + Animador |
