// Get base URL for API calls
const getBaseUrl = () => {
    const baseElement = document.querySelector('base');
    return baseElement ? baseElement.getAttribute('href') || '/' : '/';
};

// Format API URL
const formatApiUrl = (endpoint) => {
    return `${getBaseUrl()}api/${endpoint}`;
};

// Show notification
const showNotification = (message, type = 'success') => {
    const notification = document.createElement('div');
    notification.className = `fixed top-4 right-4 p-4 rounded-md text-white ${
        type === 'success' ? 'bg-green-500' : 'bg-red-500'
    } shadow-lg z-50 transition-opacity duration-500`;
    notification.textContent = message;
    
    document.body.appendChild(notification);
    
    setTimeout(() => {
        notification.style.opacity = '0';
        setTimeout(() => {
            document.body.removeChild(notification);
        }, 500);
    }, 3000);
};

// Form validation helper
const validateForm = (form) => {
    const inputs = form.querySelectorAll('input, select, textarea');
    let isValid = true;
    
    inputs.forEach(input => {
        if (input.hasAttribute('required') && !input.value.trim()) {
            isValid = false;
            input.classList.add('border-red-500');
            
            const errorMsg = input.nextElementSibling?.classList.contains('error-message') 
                ? input.nextElementSibling 
                : document.createElement('p');
                
            if (!input.nextElementSibling?.classList.contains('error-message')) {
                errorMsg.className = 'error-message text-red-500 text-sm mt-1';
                errorMsg.textContent = 'This field is required';
                input.parentNode.insertBefore(errorMsg, input.nextElementSibling);
            }
        } else {
            input.classList.remove('border-red-500');
            if (input.nextElementSibling?.classList.contains('error-message')) {
                input.parentNode.removeChild(input.nextElementSibling);
            }
        }
    });
    
    return isValid;
};

// Initialize tooltips
const initTooltips = () => {
    const tooltips = document.querySelectorAll('[data-tooltip]');
    
    tooltips.forEach(tooltip => {
        tooltip.addEventListener('mouseenter', () => {
            const tooltipText = tooltip.getAttribute('data-tooltip');
            const tooltipElement = document.createElement('div');
            tooltipElement.className = 'absolute z-10 p-2 bg-gray-800 text-white text-xs rounded shadow-lg';
            tooltipElement.textContent = tooltipText;
            tooltipElement.style.bottom = 'calc(100% + 5px)';
            tooltipElement.style.left = '50%';
            tooltipElement.style.transform = 'translateX(-50%)';
            tooltip.style.position = 'relative';
            tooltip.appendChild(tooltipElement);
        });
        
        tooltip.addEventListener('mouseleave', () => {
            const tooltipElement = tooltip.querySelector('div');
            if (tooltipElement) {
                tooltip.removeChild(tooltipElement);
            }
        });
    });
};

// Initialize on page load
document.addEventListener('DOMContentLoaded', () => {
    initTooltips();
});