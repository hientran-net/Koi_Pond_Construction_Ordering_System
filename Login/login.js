// Truy xuất các phần tử cần dùng
const wrapper = document.querySelector(".wrapper");
const loginLink = document.querySelector(".login-link");
const registerLink = document.querySelector(".register-link");
const btnPoup = document.querySelector(".btnLogin-popup");
const iconClose = document.querySelector(".icon-close");

// Sự kiện click vào link đăng ký: hiện form đăng ký, ẩn form đăng nhập
registerLink?.addEventListener("click", () => {
  wrapper?.classList.add("active");
});

// Sự kiện click vào link đăng nhập: hiện form đăng nhập, ẩn form đăng ký
loginLink?.addEventListener("click", () => {
  wrapper?.classList.remove("active");
});

// Sự kiện click vào nút đăng nhập trên header: hiển thị popup
btnPoup?.addEventListener("click", () => {
  wrapper?.classList.add("active-popup");
});

// Sự kiện click vào icon đóng: ẩn popup
iconClose?.addEventListener("click", () => {
  wrapper?.classList.remove("active-popup");
});
