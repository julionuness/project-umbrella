# Créditos e Recursos Externos

Projeto **Rodinei: Longe de casa** — equipe: Júlio César Nunes, Gustavo Vitor Ferreira e Thales Silveira de Queiroz.

Este arquivo lista os recursos de terceiros usados no jogo e o que foi produzido pela equipe.

## 1. Arte de terceiros

| Recurso | Pasta no projeto | Onde é usado | Autor / fonte | Licença |
|---|---|---|---|---|
| **Pixel Art Platformer — Village Props** | `Assets/Art/Ambient_Packs/Cainos/Pixel Art Platformer - Village Props` | Plataformas, flechas, bigornas, espinhos, pêndulo, troncos, placas, árvores e demais props do cenário | Cainos — https://cainos.itch.io/pixel-art-platformer-village-props (v2.3.1) | Uso livre em projetos gratuitos e comerciais, conforme a página do pacote |
| **Lucid Editor** (incluído no pacote da Cainos) | `Assets/Art/Ambient_Packs/Cainos/Third Party/Lucid Editor` | Ferramenta de inspetor usada pelos scripts do pacote | Annulus Games (versão modificada) — https://github.com/AnnulusGames/LucidEditor | MIT (arquivo `License.txt` na própria pasta) |
| **Pacote de personagem — City_men_2** | `Assets/Art/character_pack/City_men_2` | Sprites do Rodinei (Idle, Walk) | City Man Pixel Character - https://craftpix.net/freebies/city-man-pixel-art-character-sprite-sheets/ | Licença incluída no projeto em `Assets/_license.txt`: uso livre, inclusive comercial; edição permitida; revenda proibida; crédito não obrigatório, mas apreciado |
| **Frames de Jump do Rodinei** |  `Assets/Art/character_pack/City_men_2` | Sprites do Rodinei (jump) | Gerado pelo ChatGPT com base nas imagens do pack | 
| **Full Game Pack** | `Assets/Art/Ambient_Packs/Full Game Pack` | `Assets/Art/Prefabs` | CleanAssets — https://assetstore.unity.com/packages/2d/free-full-game-pack-369142 | Standard Unity Asset Store EULA — https://unity.com/legal/as-terms |

### Fundos de fase (parallax)

| Fase | Fundo | Pasta | Autor / fonte | Licença |
|---|---|---|---|---|
| Fase 1 | Mountain Dusk | `Assets/Art/background/MountainDusk` | ansimuz — https://ansimuz.itch.io/mountain-dusk-parallax-background | CC0 1.0 Universal — https://creativecommons.org/publicdomain/zero/1.0/ (campo "Asset license" da página) |
| Fase 2 | Glacial Mountains | `Assets/Art/background/GlacialMountains` | Vicente Nitti (vnitti) — https://vnitti.itch.io/glacial-mountains-parallax-background | Uso gratuito mediante crédito ao autor ("Art: Vicente Nitti (vnitti)"), conforme o autor na página; regras completas no pacote original |
| Fase 3 | 3_game_background | `Assets/Art/background/PNG/3_game_background` | CraftPix, Free Halloween 2D Game Backgrounds — https://craftpix.net/freebies/free-halloween-2d-game-backgrounds/ | Uso sem royalties em projetos ilimitados — https://craftpix.net/file-licenses/ |
| Fase 4 | Summer1 | `Assets/Art/background/Summer1` | Free Game Assets (CraftPix), Free Summer Pixel Backgrounds — https://free-game-assets.itch.io/free-summer-pixel-art-backgrounds | CraftPix File Licenses — https://craftpix.net/file-licenses/ (indicada pela publicadora na página) |


### Interface

| Recurso | Pasta | Autor / fonte | Licença |
|---|---|---|---|
| Ícones de teclas do tutorial (`Icon_ad`, `Icon_SpaceKey`, `Icon_WASDKeys`) | `Assets/Art/UI` | Gerado utilizando o ChatGPT | -- |

---

## 2. Áudio

| Recurso | Arquivo | Onde é usado | Autor / fonte | Licença |
|---|---|---|---|---|
| Música de fundo — **"Moment of Peace"** | `Assets/Audio/Music/mickeyscat-moment-of-peace-mickeyscat-554494.mp3` | Menu e todas as fases | MickeysCat — https://pixabay.com/music/solo-piano-moment-of-peace-mickeyscat-554494/ | Pixabay Content License — https://pixabay.com/service/license-summary/ 
| Efeito de pulo | `Assets/Audio/SFX/Jump.ogg` | Pulo e pulo duplo | Kenney, RPG Audio — https://kenney.nl/assets/rpg-audio | CC0 1.0 — https://creativecommons.org/publicdomain/zero/1.0/ |
| Passos (10 variações) | `Assets/Audio/SFX/Footsteps/footstep00.ogg` a `footstep09.ogg` | Passos do Rodinei | Kenney, RPG Audio — https://kenney.nl/assets/rpg-audio | CC0 1.0 — https://creativecommons.org/publicdomain/zero/1.0/ |

---

## 3. Fontes, interface e software

| Recurso | Uso | Licença |
|---|---|---|
| **TextMesh Pro** (Unity) | Textos da interface | Pacote da Unity |
| **Liberation Sans** | Fonte padrão do TextMesh Pro | SIL Open Font License (`Assets/TextMesh Pro/Fonts/LiberationSans - OFL.txt`) |
| **Unity 6.3 LTS** (6000.3.22f1) | Motor do jogo | Unity |
| Universal Render Pipeline 17.3.0, Input System 1.20.0, 2D Animation 13.0.5, 2D Tilemap, 2D SpriteShape, 2D PSD Importer, 2D Aseprite, Timeline 1.8.12, uGUI 2.0.0, Visual Scripting 1.9.12 | Renderização 2D, entrada, animação, terreno e interface | Pacotes da Unity |
| Git, GitHub e Git LFS | Controle de versão | — |

---

## 4. Produção da equipe

| Item | Observação |
|---|---|
| Código-fonte em C# (23 scripts em `Assets/Scripts`) | Equipe |
| Level design das 4 fases e croquis (`Docs/LevelDesign`) | Equipe; os croquis foram gerados a partir das cenas do Unity |
| Efeitos de interface por código: chuva, confete e raios de sol | Equipe, sem arte externa |
| GDD, fichas de playtest e lista de bugs | Equipe |
| Concept art, model sheet, sprite sheet e planejamento de VFX do Rodinei (`Docs/Arte`) | Equipe |
| Tela inicial (`Docs/Arte/Tela_inicial_Rodinei.png`) | Foi feita pela IA do canvas gerada a partir de imagens referências do jogo |

---

## 5. Playtesters

Agradecimento a **Cyntia Pereira Nunes** e **Letícia Rodrigues Pereira**, que testaram o jogo e cujos registros estão em `Docs/Testes`.
