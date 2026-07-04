// Funciones para generar códigos QR
window.qrCodeInterop = {
    generateQR: function (elementId, data, size) {
        try {
            const element = document.getElementById(elementId);
            if (!element) {
                console.error('Elemento no encontrado: ' + elementId);
                return;
            }

            // Limpiar contenido previo
            element.innerHTML = '';

            if (typeof QRCode !== 'undefined') {
                // Usar qrcode.js
                new QRCode(element, {
                    text: data,
                    width: size,
                    height: size,
                    colorDark: '#000000',
                    colorLight: '#ffffff',
                    correctLevel: QRCode.CorrectLevel.H
                });
            } else {
                // Fallback: mostrar el texto codificado
                element.innerHTML = '<div style="padding:20px; background:#f0f0f0; border-radius:8px; text-align:center; font-family:monospace; font-size:12px; word-break:break-all; max-width:300px;">' + 
                    'QR Code: ' + data + 
                    '</div>';
            }
        } catch (error) {
            console.error('Error al generar QR:', error);
        }
    }
};
