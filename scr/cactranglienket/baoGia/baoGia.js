// Thiết lập sự kiện khi cuộn trang
window.onscroll = function () {
  stickyMenu();
};

// Lấy phần tử có id là "menu_baogia_cuon"
var menu_baogia_cuon = document.getElementById("menu_baogia_cuon");
// Lưu lại vị trí ban đầu của thanh menu so với đầu trang
var sticky = menu_baogia_cuon.offsetTop;

// Hàm stickyMenu được gọi khi trang được cuộn
function stickyMenu() {
  // Nếu người dùng cuộn xuống quá vị trí ban đầu của thanh menu
  if (window.pageYOffset > sticky) {
    // Thêm class 'sticky' vào phần tử menu_baogia_cuon để kích hoạt chế độ cố định và đổi màu
    menu_baogia_cuon.classList.add("sticky");
  } else {
    // Nếu cuộn trở lại phía trên, xóa class 'sticky' để trả về trạng thái ban đầu
    menu_baogia_cuon.classList.remove("sticky");
  }
}
