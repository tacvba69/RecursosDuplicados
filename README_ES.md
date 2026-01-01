# Recursos Duplicados - Eliminador de Archivos Duplicados

## 📋 Descripción

**Recursos Duplicados** es una aplicación de Windows desarrollada en VB.NET que permite encontrar y eliminar archivos duplicados en tu sistema. La aplicación utiliza hash MD5 para identificar archivos idénticos y ofrece una interfaz intuitiva con soporte multiidioma.

## ✨ Características Principales

### 🔍 Búsqueda Inteligente
- Búsqueda recursiva en carpetas y subcarpetas
- Identificación precisa mediante hash MD5
- Progreso en tiempo real durante el análisis
- Soporte para todos los tipos de archivos

### 🖼️ Visualización Avanzada
- **Miniaturas reales** para imágenes y videos
- **Iconos del sistema** para otros tipos de archivos
- **Vista de iconos grandes** con zoom (Ctrl + rueda del mouse)
- **Vista de detalles** con información completa
- **Agrupación visual** de archivos duplicados

### 🎯 Selección Inteligente
- Selección automática de duplicados (mantiene una copia)
- Botones de selección rápida:
  - ✅ Seleccionar todos
  - ❌ Deseleccionar todos
  - 🔄 Invertir selección
- Estadísticas en tiempo real del espacio a liberar

### 🌍 Multiidioma
- **6 idiomas soportados:**
  - 🇪🇸 Español
  - 🇺🇸 Inglés
  - 🇫🇷 Francés
  - 🇩🇪 Alemán
  - 🇮🇹 Italiano
  - 🇵🇹 Portugués
- Selección de idioma al primer inicio
- Cambio de idioma en cualquier momento desde el menú

### 🗑️ Eliminación Segura
- Envío a la papelera de reciclaje (no eliminación permanente)
- Confirmación antes de eliminar
- Validación de permisos y rutas
- Reporte detallado de archivos eliminados

### ⚡ Optimizaciones
- Procesamiento asíncrono (no bloquea la interfaz)
- Caché inteligente de miniaturas
- Limpieza automática de memoria
- Protección contra DoS (límites de archivos)

## 🚀 Cómo Usar

### 1. Buscar Duplicados
1. Haz clic en el botón **Buscar** (📁) en la barra de herramientas
2. Selecciona la carpeta que deseas analizar
3. Espera a que termine el análisis (verás el progreso en la barra de estado)

### 2. Revisar Resultados
- Los archivos duplicados aparecen agrupados
- Cada grupo muestra cuántos archivos duplicados contiene
- Por defecto, se seleccionan automáticamente todos menos uno de cada grupo

### 3. Ajustar Selección
- Usa los checkboxes para seleccionar/deseleccionar archivos individuales
- Usa los botones de selección rápida para operaciones masivas
- Observa las estadísticas en la barra de estado

### 4. Eliminar Archivos
1. Haz clic en el botón **Eliminar** (🗑️)
2. Confirma la eliminación
3. Los archivos se enviarán a la papelera de reciclaje

## 🎨 Funcionalidades de Interfaz

### Vista de Iconos
- Muestra miniaturas grandes de los archivos
- **Zoom:** Presiona **Ctrl** y mueve la rueda del mouse para aumentar/disminuir el tamaño
- Ideal para revisar imágenes y videos

### Vista de Detalles
- Muestra información completa en formato de tabla
- Columnas: Archivo, Ruta, Tamaño, Tipo
- **Ordenamiento:** Haz clic en cualquier columna para ordenar

### Zoom de Miniaturas
- **Aumentar:** Ctrl + Rueda arriba
- **Disminuir:** Ctrl + Rueda abajo
- Rango: 64px - 256px

## 🌐 Cambiar Idioma

### Primera Vez
- Al iniciar la aplicación por primera vez, aparecerá un diálogo para seleccionar el idioma
- Elige tu idioma preferido y haz clic en "Aceptar"

### Cambiar Idioma
1. Haz clic en el botón **Idioma** (🌐) en la barra de herramientas
2. Selecciona el nuevo idioma del menú desplegable
3. Haz clic en "Aceptar"
4. El idioma se aplicará inmediatamente

## ⚙️ Requisitos del Sistema

- **Sistema Operativo:** Windows 10 o superior
- **.NET Framework:** .NET 8.0 o superior
- **Memoria:** Mínimo 2 GB RAM (recomendado 4 GB)
- **Espacio en disco:** 50 MB para la aplicación

## 🔒 Seguridad

- Validación de rutas y permisos
- Normalización de rutas para prevenir ataques
- Límites de protección contra DoS
- Confirmación antes de eliminar archivos
- Eliminación segura a la papelera (recuperable)

## 📊 Límites y Protecciones

- **Máximo de archivos:** 50,000 (con advertencia)
- **Tamaño máximo de archivo:** 50 GB
- **Caché de imágenes:** 50,000 entradas (limpieza automática)
- **ImageList:** 50,000 imágenes (limpieza inteligente)

## 🐛 Solución de Problemas

### La aplicación no encuentra duplicados
- Verifica que tengas permisos de lectura en la carpeta
- Asegúrate de que haya archivos duplicados realmente
- Algunos archivos pueden estar en uso o bloqueados

### Las miniaturas no se muestran
- Verifica que los archivos existan
- Algunos formatos pueden no tener soporte de miniaturas
- Intenta regenerar las miniaturas cambiando el zoom

### Error al eliminar archivos
- Verifica que tengas permisos de escritura
- Asegúrate de que los archivos no estén en uso
- Algunos archivos del sistema no se pueden eliminar

## 📝 Notas

- Los archivos eliminados van a la papelera de reciclaje y pueden recuperarse
- El análisis puede tardar en carpetas con muchos archivos
- Se recomienda cerrar otros programas durante el análisis intensivo
- Las miniaturas se generan la primera vez y se almacenan en caché

## 👨‍💻 Desarrollo

Desarrollado en Visual Basic .NET (.NET 8.0)
- Interfaz: Windows Forms
- Hash: MD5 para identificación de duplicados
- Miniaturas: Windows Shell API

## 📄 Licencia

Este proyecto es de código abierto y está disponible para uso personal y comercial.

---

**Versión:** 1.0  
**Última actualización:** 2024

