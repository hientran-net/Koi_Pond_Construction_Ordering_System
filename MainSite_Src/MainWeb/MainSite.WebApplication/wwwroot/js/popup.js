const popup = document.getElementById("popup");
const popupImage = document.getElementById("popupImage");
const popupTitle = document.getElementById("popupTitle");
const popupDescription = document.getElementById("popupDescription");
const closeBtn = document.querySelector(".close-btn");

// Show popup on image click
document.querySelectorAll(".gallery img").forEach((img) => {
  img.addEventListener("click", () => {
    popup.style.display = "flex";
    popupImage.src = img.src;
    popupTitle.textContent = img.dataset.title;
    popupDescription.textContent = img.dataset.description;
  });
});

// Close popup
closeBtn.addEventListener("click", () => {
  popup.style.display = "none";
});

// Close popup when clicking outside content
popup.addEventListener("click", (e) => {
  if (e.target === popup) {
    popup.style.display = "none";
  }
});
