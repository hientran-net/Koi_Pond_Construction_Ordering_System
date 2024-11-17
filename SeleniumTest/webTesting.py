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
        userName.send_keys("hien")
        time.sleep(3)
        
        # Tìm element password bằng ID
        password = driver.find_element(By.ID, "password")
        password.send_keys("admin")
        time.sleep(3)
        password.send_keys(Keys.RETURN)
        time.sleep(3)

        driver.get("http://localhost:5233/Employee/Index")
        time.sleep(3)
        driver.get("http://localhost:5233/Employee/Create")
        time.sleep(3)

        enterName = driver.find_element(By.ID, "NhanVien_HoTen")
        enterName.send_keys("Trần Minh Hiền")
        time.sleep(3)
        
        enterMail = driver.find_element(By.ID, "NhanVien_Email")
        enterMail.send_keys("hientm0978@ut.edu.vn")
        time.sleep(3)

        enterPhonenumber = driver.find_element(By.ID, "NhanVien_SoDienThoai")
        enterPhonenumber.send_keys("0559800767")
        time.sleep(3)

        enterAddress = driver.find_element(By.ID, "NhanVien_DiaChia")
        enterAddress.send_keys("Bến Tre")
        time.sleep(3)


        enterAddress.send_keys(Keys.RETURN)

       
       


    def tearDown(self):
        time.sleep(30)  # Giảm thời gian chờ xuống 10 giây
        self.driver.close()

if __name__ == "__main__":
    unittest.main()