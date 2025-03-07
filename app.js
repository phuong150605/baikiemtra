// Các biến toàn cục
const API_URL = 'https://localhost:7257/api/HangHoa';
let isEditing = false;
let currentId = '';

// DOM elements
const hangHoaForm = document.getElementById('hangHoaForm');
const formTitle = document.getElementById('formTitle');
const maHangHoaInput = document.getElementById('ma_hanghoa');
const tenHangHoaInput = document.getElementById('ten_hanghoa');
const soLuongInput = document.getElementById('so_luong');
const ghiChuInput = document.getElementById('ghi_chu');
const resetBtn = document.getElementById('resetBtn');
const hangHoaList = document.getElementById('hangHoaList');

// Load dữ liệu khi trang được tải
document.addEventListener('DOMContentLoaded', loadHangHoa);

// Xử lý sự kiện submit form
hangHoaForm.addEventListener('submit', function(e) {
    e.preventDefault();
    
    const hangHoa = {
        ma_hanghoa: maHangHoaInput.value,
        ten_hanghoa: tenHangHoaInput.value,
        so_luong: parseInt(soLuongInput.value),
        ghi_chu: ghiChuInput.value
    };
    
    if (isEditing) {
        updateHangHoa(currentId, hangHoa);
    } else {
        createHangHoa(hangHoa);
    }
});

// Reset form
resetBtn.addEventListener('click', resetForm);

// CRUD Operations

// Lấy danh sách hàng hóa
async function loadHangHoa() {
    try {
        const response = await fetch(API_URL);
        const data = await response.json();
        displayHangHoa(data);
    } catch (error) {
        console.error('Error loading data:', error);
        alert('Không thể tải dữ liệu. Vui lòng thử lại sau.');
    }
}

// Hiển thị danh sách hàng hóa
function displayHangHoa(hangHoaList) {
    const tableBody = document.getElementById('hangHoaList');
    tableBody.innerHTML = '';
    
    hangHoaList.forEach(item => {
        const row = document.createElement('tr');
        row.innerHTML = `
            <td>${item.ma_hanghoa}</td>
            <td>${item.ten_hanghoa}</td>
            <td>${item.so_luong}</td>
            <td>${item.ghi_chu || ''}</td>
            <td>
                <button class="btn btn-sm btn-warning me-1" onclick="editHangHoa('${item.ma_hanghoa}')">Sửa</button>
                <button class="btn btn-sm btn-danger" onclick="deleteHangHoa('${item.ma_hanghoa}')">Xóa</button>
            </td>
        `;
        tableBody.appendChild(row);
    });
}

// Thêm mới hàng hóa
async function createHangHoa(hangHoa) {
    try {
        const response = await fetch(API_URL, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(hangHoa)
        });
        
        if (!response.ok) {
            const errorData = await response.json();
            throw new Error(errorData.message || 'Could not create item');
        }
        
        resetForm();
        loadHangHoa();
        alert('Thêm hàng hóa thành công!');
    } catch (error) {
        console.error('Error creating item:', error);
        alert(`Lỗi: ${error.message}`);
    }
}

// Lấy thông tin hàng hóa theo mã
async function getHangHoa(id) {
    try {
        const response = await fetch(`${API_URL}/${id}`);
        
        if (!response.ok) {
            throw new Error('Could not find item');
        }
        
        return await response.json();
    } catch (error) {
        console.error('Error getting item:', error);
        alert(`Lỗi: ${error.message}`);
        return null;
    }
}

// Chuẩn bị form để chỉnh sửa
async function editHangHoa(id) {
    const hangHoa = await getHangHoa(id);
    
    if (!hangHoa) return;
    
    isEditing = true;
    currentId = id;
    formTitle.textContent = 'Chỉnh Sửa Hàng Hóa';
    
    maHangHoaInput.value = hangHoa.ma_hanghoa;
    maHangHoaInput.readOnly = true;
    tenHangHoaInput.value = hangHoa.ten_hanghoa;
    soLuongInput.value = hangHoa.so_luong;
    ghiChuInput.value = hangHoa.ghi_chu || '';
}

// Cập nhật hàng hóa
async function updateHangHoa(id, hangHoa) {
    try {
        const response = await fetch(`${API_URL}/${id}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(hangHoa)
        });
        
        if (!response.ok) {
            const errorData = await response.json();
            throw new Error(errorData.message || 'Could not update item');
        }
        
        resetForm();
        loadHangHoa();
        alert('Cập nhật hàng hóa thành công!');
    } catch (error) {
        console.error('Error updating item:', error);
        alert(`Lỗi: ${error.message}`);
    }
}

// Xóa hàng hóa
async function deleteHangHoa(id) {
    if (!confirm('Bạn có chắc chắn muốn xóa hàng hóa này?')) {
        return;
    }
    
    try {
        const response = await fetch(`${API_URL}/${id}`, {
            method: 'DELETE'
        });
        
        if (!response.ok) {
            throw new Error('Could not delete item');
        }
        
        loadHangHoa();
        alert('Xóa hàng hóa thành công!');
    } catch (error) {
        console.error('Error deleting item:', error);
        alert(`Lỗi: ${error.message}`);
    }
}

// Reset form về trạng thái ban đầu
function resetForm() {
    hangHoaForm.reset();
    isEditing = false;
    currentId = '';
    formTitle.textContent = 'Thêm Hàng Hóa Mới';
    maHangHoaInput.readOnly = false;
}