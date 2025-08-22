let tabs = [];
let currentTabIndex = null;

// ======== INIT SAAT HALAMAN DIMUAT ========
window.addEventListener('DOMContentLoaded', () => {
    const savedTabs = JSON.parse(localStorage.getItem('formPenyesuaianTabs') || '[]');
    const savedIndex = parseInt(localStorage.getItem('formPenyesuaianActiveTab') || '0', 10);

    if (savedTabs.length > 0) {
        tabs = savedTabs;
        currentTabIndex = Math.min(savedIndex, tabs.length - 1); // pastikan index valid
        loadFormFromTab();
        restoreStatusUI();
        renderTabs();
    } else {
        createNewTab();
    }
});

// ======== SIMPAN DATA SAAT REFRESH / CLOSE ========
window.addEventListener('beforeunload', () => {
    saveCurrentForm();
    saveTabs();
});

// ======== SET STATUS TAB AKTIF ========
function setCurrentStatForActiveTab(stat) {
    currentstat = stat;
    if (currentTabIndex !== null && tabs[currentTabIndex]) {
        tabs[currentTabIndex].currentstat = stat;
        saveTabs();
    }
}

// ======== BUAT TAB BARU ========
function createNewTab() {
    if (currentTabIndex !== null) {
        saveCurrentForm(); // simpan tab lama dulu
    }

    const newTab = {
        id: Date.now(),
        formData: {},
        dataPenyesuaian: [],
        currentstat: null
    };
    tabs.push(newTab);
    currentTabIndex = tabs.length - 1;
    localStorage.setItem('formPenyesuaianActiveTab', currentTabIndex);
    saveTabs();
    renderTabs();
    clearForm();
    if (typeof initializeFormButtons === 'function') initializeFormButtons();
}

// ======== RENDER TABS DI UI ========
function renderTabs() {
    const tabBar = document.getElementById('tabBar');
    if (!tabBar) return;
    tabBar.innerHTML = '';

    tabs.forEach((tab, index) => {
        const btn = document.createElement('button');
        btn.textContent = `Nota ${index + 1}`;

        // Tambah class active untuk tab aktif
        /*if (index === currentTabIndex) {
            btn.classList.add('active');
        }*/

        if (index === currentTabIndex) {
            btn.style.background = '#6badf3ff';
        } else if (tab.currentstat === 'tambah') {
            btn.style.background = '#28a745'; // hijau untuk status tambah
        } else {
            btn.style.background = '#6c757d';
        }

        btn.onclick = () => switchTab(index);

        const closeBtn = document.createElement('span');
        closeBtn.textContent = '×';
        closeBtn.onclick = (e) => {
            e.stopPropagation();
            removeTab(index);
        };
        btn.appendChild(closeBtn);

        tabBar.appendChild(btn);
    });

    const addBtn = document.createElement('button');
    addBtn.textContent = '+';
    addBtn.style.background = '#6c757d';
    addBtn.onclick = createNewTab;
    tabBar.appendChild(addBtn);
}


// ======== PINDAH TAB ========
function switchTab(index) {
    saveCurrentForm();
    currentTabIndex = index;
    localStorage.setItem('formPenyesuaianActiveTab', index);
    loadFormFromTab();
    restoreStatusUI();
    renderTabs();
}

// ======== PULIHKAN STATUS UI SESUAI TAB ========
function restoreStatusUI() {
    const tabData = tabs[currentTabIndex];
    currentstat = tabData.currentstat || null;
    if (currentstat === 'tambah') {
        if (typeof laststat === 'function') laststat();
    } else {
        if (typeof initializeFormButtons === 'function') initializeFormButtons();
    }
}

// ======== LOAD FORM DARI TAB ========
function loadFormFromTab() {
    if (currentTabIndex === null) return;
    const tabData = tabs[currentTabIndex];
    clearForm();

    Object.keys(tabData.formData || {}).forEach(key => {
        const el = document.getElementById(key);
        if (el) {
            el.value = tabData.formData[key].value;
            el.disabled = tabData.formData[key].disabled;
        }
    });

    dataPenyesuaian = [...(tabData.dataPenyesuaian || [])];
    renderTabelPenyesuaian();
}

// ======== SIMPAN FORM KE TAB AKTIF ========
function saveCurrentForm() {
    if (currentTabIndex === null || !tabs[currentTabIndex]) return;
    const form = document.getElementById('formPenyesuaian');
    const formData = {};
    form.querySelectorAll('input, select, textarea, button').forEach(el => {
        if (el.id) {
            formData[el.id] = { value: el.value, disabled: el.disabled };
        }
    });

    tabs[currentTabIndex].formData = formData;
    tabs[currentTabIndex].dataPenyesuaian = [...dataPenyesuaian];
    tabs[currentTabIndex].currentstat = currentstat;
    saveTabs();
}

// ======== SIMPAN SEMUA TAB KE localStorage ========
function saveTabs() {
    localStorage.setItem('formPenyesuaianTabs', JSON.stringify(tabs));
}

// ======== HAPUS TAB ========
function removeTab(index) {
    tabs.splice(index, 1);
    if (tabs.length === 0) {
        currentTabIndex = null;
        localStorage.removeItem('formPenyesuaianActiveTab');
        clearForm();
    } else {
        currentTabIndex = Math.max(0, index - 1);
        localStorage.setItem('formPenyesuaianActiveTab', currentTabIndex);
        loadFormFromTab();
        restoreStatusUI();
    }
    saveTabs();
    renderTabs();
}

// ======== BERSIHKAN FORM ========
function clearForm() {
    const formElements = document.querySelectorAll('#formPenyesuaian input, #formPenyesuaian select, #formPenyesuaian textarea');
    formElements.forEach(el => {
        if (el.type !== 'button' && el.type !== 'submit') {
            el.value = '';
            el.disabled = false;
        }
    });
    dataPenyesuaian = [];
}
