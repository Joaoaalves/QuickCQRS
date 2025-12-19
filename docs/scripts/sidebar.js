function highlightSidebarItem() {
    const sidebarItems = document.querySelectorAll('.sidebar-item');
    const currentPath = window.location.pathname;

    sidebarItems.forEach(item => {
        const itemPath = item.getAttribute('data-path');
        if (itemPath === currentPath || currentPath.endsWith(itemPath)) {
            item.classList.add('active');
        }
    });
}

document.addEventListener('DOMContentLoaded', highlightSidebarItem);