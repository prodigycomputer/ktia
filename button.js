let currentstat = null;
function initializeFormButtons(config = {}) {
  currentstat = null;

  // --- Default daftar tombol yang pasti ada ---
  const defaultButtons = ["btnTambah", "btnEdit", "btnHapus", "btnCancel", "btnSave"];
  const extraButtons = config.extraButtons || []; // contoh: ["btnHarga"]

  // --- Atur tombol ---
  defaultButtons.forEach(id => {
    const el = document.getElementById(id);
    if (el) el.disabled = (id !== "btnTambah"); // hanya btnTambah aktif
  });
  extraButtons.forEach(id => {
    const el = document.getElementById(id);
    if (el) el.disabled = true;
  });

  // --- Disable semua input sesuai daftar ---
  (config.fields || []).forEach(field => {
    let el, disabled = true, readonly;
    if (typeof field === "string") {
      el = document.getElementById(field);
    } else if (typeof field === "object" && field.id) {
      el = document.getElementById(field.id);
      if (field.disabled !== undefined) disabled = field.disabled;
      if (field.readonly !== undefined) readonly = field.readonly;
    }
    if (el) {
      el.disabled = disabled;
      if (readonly !== undefined) el.readOnly = readonly;
    }
  });

  // --- Reset search box ---
  const searchKode = document.getElementById("searchKode");
  const searchNama = document.getElementById("searchNama");
  const searchBtn  = document.getElementById("searchbtn");

  if (searchKode) { searchKode.value = ""; searchKode.disabled = false; }
  if (searchNama) { searchNama.value = ""; searchNama.disabled = false; }
  if (searchBtn)  searchBtn.disabled = false;

  if (typeof resetButtonStyles === "function") {
    resetButtonStyles();
  }
}
