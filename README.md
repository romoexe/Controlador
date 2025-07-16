# Controlador
# Welcome to GitHub Desktop!
# 🤚 Gesture Detector en C#

Este módulo implementa un sistema básico de detección e interpretación de gestos de manos. Es una conversión desde Python (Mediapipe + OpenCV) a C# utilizando **Emgu CV** como binding de OpenCV.

## 📂 Estructura

- `GestureDetector`: Orquesta la detección de manos, la interpretación de dedos levantados y decide qué acción corresponde.
- `HandProcessing`: Encapsula la lógica para detectar manos, extraer puntos clave, determinar qué dedos están levantados y calcular distancias.
- `DrawingFunctions`: Superpone imágenes de acciones (adelante, atrás, izquierda, derecha, stop) sobre el fotograma original para mostrar visualmente el gesto detectado.

## ⚙️ Dependencias

- [.NET 6.0+](https://dotnet.microsoft.com/)
- [Emgu CV](https://www.emgu.com/) (binding de OpenCV para C#)
- (Opcional) Algún detector de manos compatible. *Nota:* Mediapipe no tiene soporte oficial en C#, deberás sustituirlo por un modelo propio o una librería de terceros.

## 📸 Flujo general

1. **Captura de frame:** Se recibe una imagen de cámara o video.
2. **Procesamiento de manos:** `HandProcessing` encuentra manos y extrae puntos clave.
3. **Interpretación:** `GestureDetector` usa la lista de dedos levantados para determinar una acción (`A`, `P`, `I`, `D`, `R`).
4. **Visualización:** `DrawingFunctions` dibuja la imagen de acción sobre el frame original.
