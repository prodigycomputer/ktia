// notif.js
function showToast(message, bgColor = '#28a745') {
    const toast = document.getElementById('toast');
    if (!toast) return;

    toast.innerText = message;
    toast.style.backgroundColor = bgColor;
    toast.style.display = 'block';

    setTimeout(() => {
        toast.style.display = 'none';
    }, 3000);
}

window.addEventListener('DOMContentLoaded', () => {
    const params = new URLSearchParams(window.location.search);
    const status = params.get('status');

    if (status) {
        let message = '';
        let color = '#28a745';

        switch (status) {
            case 'tambah':
                message = 'Data berhasil ditambahkan!';
                break;
            case 'update':
                message = 'Data berhasil diperbarui!';
                break;
            case 'hapus':
                message = 'Data berhasil dihapus!';
                break;
            case 'error':
                message = 'Terjadi kesalahan saat memproses data.';
                color = '#dc3545';
                break;
            case 'duplikat':
                message = 'Kode sudah terdaftar!';
                color = '#ffc107';
                break;
            default:
                return;
        }

        showToast(message, color);

        // Hapus parameter dari URL
        history.replaceState(null, '', window.location.pathname);
    }
});
