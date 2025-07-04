document.addEventListener("DOMContentLoaded", () => {
    const swipeData = window.swipeData || [];

    // Group data by flag
    const flagCounts = {};
    swipeData.forEach(entry => {
        flagCounts[entry.flag] = (flagCounts[entry.flag] || 0) + 1;
    });

    const flags = Object.keys(flagCounts);
    const counts = Object.values(flagCounts);

    const ctx = document.getElementById('flagChart').getContext('2d');

    const myChart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: flags,
            datasets: [{
                label: 'People Present',
                data: counts,
                backgroundColor: ['#4e79a7', '#f28e2b', '#e15759', '#76b7b2'],
                borderColor: '#333',
                borderWidth: 1
            }]
        },
        options: {
            responsive: true,
            plugins: {
                title: {
                    display: true,
                    text: 'People Present by Flag'
                }
            }
        }
    });
});
