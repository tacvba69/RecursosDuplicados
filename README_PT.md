# Recursos Duplicados - Removedor de Arquivos Duplicados

## 📋 Descrição

**Recursos Duplicados** é uma aplicação Windows desenvolvida em VB.NET que permite encontrar e excluir arquivos duplicados no seu sistema. A aplicação utiliza hash MD5 para identificar arquivos idênticos e oferece uma interface intuitiva com suporte multilíngue.

## ✨ Características Principais

### 🔍 Busca Inteligente
- Busca recursiva em pastas e subpastas
- Identificação precisa usando hash MD5
- Progresso em tempo real durante a análise
- Suporte para todos os tipos de arquivo

### 🖼️ Visualização Avançada
- **Miniaturas reais** para imagens e vídeos
- **Ícones do sistema** para outros tipos de arquivo
- **Vista de ícones grandes** com zoom (Ctrl + roda do mouse)
- **Vista de detalhes** com informações completas
- **Agrupamento visual** de arquivos duplicados

### 🎯 Seleção Inteligente
- Seleção automática de duplicados (mantém uma cópia)
- Botões de seleção rápida:
  - ✅ Selecionar todos
  - ❌ Desselecionar todos
  - 🔄 Inverter seleção
- Estatísticas em tempo real do espaço a ser liberado

### 🌍 Multilíngue
- **6 idiomas suportados:**
  - 🇪🇸 Espanhol
  - 🇺🇸 Inglês
  - 🇫🇷 Francês
  - 🇩🇪 Alemão
  - 🇮🇹 Italiano
  - 🇵🇹 Português
- Seleção de idioma no primeiro início
- Alterar idioma a qualquer momento no menu

### 🗑️ Exclusão Segura
- Envio para a lixeira (não exclusão permanente)
- Confirmação antes de excluir
- Validação de permissões e caminhos
- Relatório detalhado de arquivos excluídos

### ⚡ Otimizações
- Processamento assíncrono (não bloqueia a interface)
- Cache inteligente de miniaturas
- Limpeza automática de memória
- Proteção contra DoS (limites de arquivos)

## 🚀 Como Usar

### 1. Buscar Duplicados
1. Clique no botão **Buscar** (📁) na barra de ferramentas
2. Selecione a pasta que deseja analisar
3. Aguarde a conclusão da análise (você verá o progresso na barra de status)

### 2. Revisar Resultados
- Os arquivos duplicados aparecem agrupados
- Cada grupo mostra quantos arquivos duplicados contém
- Por padrão, todos exceto um arquivo de cada grupo são selecionados automaticamente

### 3. Ajustar Seleção
- Use as caixas de seleção para selecionar/desselecionar arquivos individuais
- Use os botões de seleção rápida para operações em massa
- Observe as estatísticas na barra de status

### 4. Excluir Arquivos
1. Clique no botão **Excluir** (🗑️)
2. Confirme a exclusão
3. Os arquivos serão enviados para a lixeira

## 🎨 Funcionalidades da Interface

### Vista de Ícones
- Mostra miniaturas grandes dos arquivos
- **Zoom:** Pressione **Ctrl** e mova a roda do mouse para aumentar/diminuir o tamanho
- Ideal para revisar imagens e vídeos

### Vista de Detalhes
- Mostra informações completas em formato de tabela
- Colunas: Arquivo, Caminho, Tamanho, Tipo
- **Ordenação:** Clique em qualquer coluna para ordenar

### Zoom de Miniaturas
- **Aumentar:** Ctrl + Roda para cima
- **Diminuir:** Ctrl + Roda para baixo
- Intervalo: 64px - 256px

## 🌐 Alterar Idioma

### Primeira Vez
- Ao iniciar a aplicação pela primeira vez, aparecerá uma caixa de diálogo para selecionar o idioma
- Escolha seu idioma preferido e clique em "Aceitar"

### Alterar Idioma
1. Clique no botão **Idioma** (🌐) na barra de ferramentas
2. Selecione o novo idioma no menu suspenso
3. Clique em "Aceitar"
4. O idioma será aplicado imediatamente

## ⚙️ Requisitos do Sistema

- **Sistema Operacional:** Windows 10 ou superior
- **.NET Framework:** .NET 8.0 ou superior
- **Memória:** Mínimo 2 GB RAM (4 GB recomendado)
- **Espaço em disco:** 50 MB para a aplicação

## 🔒 Segurança

- Validação de caminhos e permissões
- Normalização de caminhos para prevenir ataques
- Limites de proteção contra DoS
- Confirmação antes de excluir arquivos
- Exclusão segura para a lixeira (recuperável)

## 📊 Limites e Proteções

- **Máximo de arquivos:** 50.000 (com aviso)
- **Tamanho máximo de arquivo:** 50 GB
- **Cache de imagens:** 50.000 entradas (limpeza automática)
- **ImageList:** 50.000 imagens (limpeza inteligente)

## 🐛 Solução de Problemas

### A aplicação não encontra duplicados
- Verifique se você tem permissões de leitura na pasta
- Certifique-se de que há arquivos duplicados realmente
- Alguns arquivos podem estar em uso ou bloqueados

### As miniaturas não são exibidas
- Verifique se os arquivos existem
- Alguns formatos podem não ter suporte a miniaturas
- Tente regenerar as miniaturas alterando o zoom

### Erro ao excluir arquivos
- Verifique se você tem permissões de gravação
- Certifique-se de que os arquivos não estão em uso
- Alguns arquivos do sistema não podem ser excluídos

## 📝 Notas

- Os arquivos excluídos vão para a lixeira e podem ser recuperados
- A análise pode levar tempo em pastas com muitos arquivos
- É recomendado fechar outros programas durante a análise intensiva
- As miniaturas são geradas na primeira vez e armazenadas em cache

## 👨‍💻 Desenvolvimento

Desenvolvido em Visual Basic .NET (.NET 8.0)
- Interface: Windows Forms
- Hash: MD5 para identificação de duplicados
- Miniaturas: Windows Shell API

## 📄 Licença

Este projeto é open source e está disponível para uso pessoal e comercial.

---

**Versão:** 1.0  
**Última atualização:** 2024

