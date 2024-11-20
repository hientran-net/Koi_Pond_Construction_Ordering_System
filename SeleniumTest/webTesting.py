import unittest
from selenium import webdriver
from selenium.webdriver.common.keys import Keys
from selenium.webdriver.common.by import By
import time

class ChromeSearch(unittest.TestCase):
    def setUp(self):
        self.driver = webdriver.Chrome()
        
    def test_search_in_python_org(self):
        driver = self.driver
        driver.get("http://localhost:5233/")
        
        # Tìm element username bằng ID
        userName = driver.find_element(By.ID, "userName")
        userName.send_keys("admin")
        time.sleep(3)
        
        # Tìm element password bằng ID
        password = driver.find_element(By.ID, "password")
        password.send_keys("admin")
        time.sleep(3)
        password.send_keys(Keys.RETURN)
        time.sleep(3)

        driver.get("http://localhost:5233/CustomerManage/Index")
        time.sleep(3)
        driver.get("http://localhost:5233/CustomerManage/Create")
        time.sleep(3)

        enterName = driver.find_element(By.ID, "KhachHang_TenKhachHang")
        enterName.send_keys("Đỗ Đức Anh")
        time.sleep(3)
        
        enterMail = driver.find_element(By.ID, "KhachHang_DiaChi")
        enterMail.send_keys("112 Đường Láng Hạ, Quận Đống Đa, Hà Nội")
        time.sleep(3)

        enterPhonenumber = driver.find_element(By.ID, "KhachHang_SoDienThoai")
        enterPhonenumber.send_keys("0989012345")
        time.sleep(3)

        enterAddress = driver.find_element(By.ID, "KhachHang_Email")
        enterAddress.send_keys("ducanh.do@gmail.com")
        time.sleep(3)


        enterAddress.send_keys(Keys.RETURN)

       
       


    def tearDown(self):
        time.sleep(30)  # Giảm thời gian chờ xuống 10 giây
        self.driver.close()

if __name__ == "__main__":
    unittest.main()