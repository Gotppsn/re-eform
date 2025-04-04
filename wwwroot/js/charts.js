/**
 * Charts and data visualization for reports
 */

// Create a submissions over time chart
function createSubmissionsChart(elementId, data) {
    // Get the canvas element
    const canvas = document.getElementById(elementId);
    if (!canvas) return;
    
    // Extract data for the chart
    const labels = data.map(item => {
        const date = new Date(item.date);
        return date.toLocaleDateString('en-US', { month: 'short', day: 'numeric' });
    });
    
    const counts = data.map(item => item.count);
    
    // Create the chart using Chart.js
    new Chart(canvas, {
        type: 'line',
        data: {
            labels: labels,
            datasets: [{
                label: 'Submissions',
                data: counts,
                backgroundColor: 'rgba(99, 102, 241, 0.2)',
                borderColor: 'rgba(99, 102, 241, 1)',
                borderWidth: 2,
                tension: 0.4,
                pointBackgroundColor: 'rgba(99, 102, 241, 1)'
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            scales: {
                y: {
                    beginAtZero: true,
                    ticks: {
                        precision: 0
                    }
                }
            },
            plugins: {
                legend: {
                    display: false
                },
                tooltip: {
                    callbacks: {
                        title: function(tooltipItems) {
                            return tooltipItems[0].label;
                        },
                        label: function(context) {
                            return `Submissions: ${context.parsed.y}`;
                        }
                    }
                }
            }
        }
    });
}

// Create a top forms chart
function createTopFormsChart(elementId, data) {
    // Get the canvas element
    const canvas = document.getElementById(elementId);
    if (!canvas) return;
    
    // Sort data by submission count
    data.sort((a, b) => b.submissions - a.submissions);
    
    // Take only the top 5 forms
    const topForms = data.slice(0, 5);
    
    // Extract data for the chart
    const labels = topForms.map(form => form.title);
    const counts = topForms.map(form => form.submissions);
    
    // Create the chart using Chart.js
    new Chart(canvas, {
        type: 'bar',
        data: {
            labels: labels,
            datasets: [{
                label: 'Submissions',
                data: counts,
                backgroundColor: [
                    'rgba(99, 102, 241, 0.8)',
                    'rgba(20, 184, 166, 0.8)',
                    'rgba(245, 158, 11, 0.8)',
                    'rgba(239, 68, 68, 0.8)',
                    'rgba(139, 92, 246, 0.8)'
                ],
                borderWidth: 0
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            indexAxis: 'y',
            scales: {
                x: {
                    beginAtZero: true,
                    ticks: {
                        precision: 0
                    }
                }
            },
            plugins: {
                legend: {
                    display: false
                },
                tooltip: {
                    callbacks: {
                        label: function(context) {
                            return `Submissions: ${context.parsed.x}`;
                        }
                    }
                }
            }
        }
    });
}

// Create a question response chart
function createQuestionChart(elementId, questionType, data) {
    // Get the canvas element
    const canvas = document.getElementById(elementId);
    if (!canvas) return;
    
    let chartType, chartData, chartOptions;
    
    // Configure chart based on question type
    if (questionType === 'radio' || questionType === 'select') {
        // Count occurrences of each answer
        const answerCounts = {};
        data.forEach(answer => {
            answerCounts[answer] = (answerCounts[answer] || 0) + 1;
        });
        
        // Prepare data for chart
        const labels = Object.keys(answerCounts);
        const counts = Object.values(answerCounts);
        
        // Create a pie chart for single-select questions
        chartType = 'pie';
        chartData = {
            labels: labels,
            datasets: [{
                data: counts,
                backgroundColor: [
                    'rgba(99, 102, 241, 0.8)',
                    'rgba(20, 184, 166, 0.8)',
                    'rgba(245, 158, 11, 0.8)',
                    'rgba(239, 68, 68, 0.8)',
                    'rgba(139, 92, 246, 0.8)',
                    'rgba(75, 85, 99, 0.8)'
                ],
                borderWidth: 1
            }]
        };
        chartOptions = {
            responsive: true,
            maintainAspectRatio: false,
            plugins: {
                tooltip: {
                    callbacks: {
                        label: function(context) {
                            const label = context.label || '';
                            const value = context.parsed || 0;
                            const total = context.dataset.data.reduce((acc, val) => acc + val, 0);
                            const percentage = total ? Math.round((value / total) * 100) : 0;
                            return `${label}: ${value} (${percentage}%)`;
                        }
                    }
                }
            }
        };
    } else if (questionType === 'checkbox') {
        // Count occurrences of each option
        const answerCounts = {};
        Object.keys(data[0]).forEach(key => {
            answerCounts[key] = 0;
            data.forEach(answer => {
                if (answer[key] === 'true') {
                    answerCounts[key]++;
                }
            });
        });
        
        // Prepare data for chart
        const labels = Object.keys(answerCounts);
        const counts = Object.values(answerCounts);
        
        // Create a bar chart for multi-select questions
        chartType = 'bar';
        chartData = {
            labels: labels,
            datasets: [{
                label: 'Responses',
                data: counts,
                backgroundColor: 'rgba(99, 102, 241, 0.8)',
                borderWidth: 0
            }]
        };
        chartOptions = {
            responsive: true,
            maintainAspectRatio: false,
            indexAxis: 'y',
            scales: {
                x: {
                    beginAtZero: true,
                    ticks: {
                        precision: 0
                    }
                }
            },
            plugins: {
                legend: {
                    display: false
                },
                tooltip: {
                    callbacks: {
                        label: function(context) {
                            return `Responses: ${context.parsed.x}`;
                        }
                    }
                }
            }
        };
    } else if (questionType === 'text' || questionType === 'textarea') {
        // For text inputs, show a word cloud or text response list
        // For this example, we'll just show a placeholder
        const container = canvas.parentElement;
        container.innerHTML = `
            <div class="p-4 bg-gray-50 rounded border border-gray-200 h-full flex flex-col">
                <div class="text-gray-500 mb-2">Text responses (${data.length})</div>
                <div class="overflow-y-auto flex-grow">
                    <ul class="space-y-2">
                        ${data.slice(0, 5).map(response => `
                            <li class="p-2 bg-white rounded border border-gray-200">${response}</li>
                        `).join('')}
                        ${data.length > 5 ? `<li class="text-center text-gray-500 mt-2">+ ${data.length - 5} more responses</li>` : ''}
                    </ul>
                </div>
            </div>
        `;
        return;
    } else {
        // Default chart for other question types
        return;
    }
    
    // Create the chart
    new Chart(canvas, {
        type: chartType,
        data: chartData,
        options: chartOptions
    });
}

// Initialize charts when the page loads
document.addEventListener('DOMContentLoaded', () => {
    // Check if we're on the reports page
    const reportsContainer = document.getElementById('reports-container');
    if (!reportsContainer) return;
    
    // Example data for charts
    const submissionsData = [
        { date: '2025-03-28', count: 12 },
        { date: '2025-03-29', count: 18 },
        { date: '2025-03-30', count: 15 },
        { date: '2025-03-31', count: 22 },
        { date: '2025-04-01', count: 16 },
        { date: '2025-04-02', count: 19 },
        { date: '2025-04-03', count: 22 }
    ];
    
    const topFormsData = [
        { title: 'Customer Feedback', submissions: 56 },
        { title: 'IT Support Request', submissions: 87 },
        { title: 'Leave Request', submissions: 124 },
        { title: 'Equipment Request', submissions: 36 },
        { title: 'Travel Expense Claim', submissions: 84 },
        { title: 'Employee Onboarding', submissions: 0 }
    ];
    
    // Initialize charts
    const submissionsChartEl = document.getElementById('submissions-chart');
    if (submissionsChartEl) {
        createSubmissionsChart('submissions-chart', submissionsData);
    }
    
    const topFormsChartEl = document.getElementById('top-forms-chart');
    if (topFormsChartEl) {
        createTopFormsChart('top-forms-chart', topFormsData);
    }
    
    // Initialize question charts for form reports
    const questionCharts = document.querySelectorAll('[data-question-chart]');
    questionCharts.forEach(chartEl => {
        const questionType = chartEl.dataset.questionType;
        const questionData = JSON.parse(chartEl.dataset.questionData || '[]');
        createQuestionChart(chartEl.id, questionType, questionData);
    });
});