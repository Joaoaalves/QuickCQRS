function highlightSidebarItem() {
    const sidebarItems = document.querySelectorAll('.sidebar-item');
    const currentPath = window.location.pathname;

    sidebarItems.forEach(item => {
        const itemPath = item.getAttribute('data-path');
        if (itemPath === currentPath) {
            item.classList.add('active');
        }
    });
}

document.addEventListener("DOMContentLoaded", () => {
    fetch("./components/sidebar.html")
        .then(response => response.text())
        .then(html => {
            document.getElementById("sidebar").innerHTML = html;
            highlightSidebarItem();
        })
        .catch(err => console.error("Error loading sidebar:", err));
});