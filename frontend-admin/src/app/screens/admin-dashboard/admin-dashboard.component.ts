import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Chart, registerables } from 'chart.js';
import { AdminAnalyticsService, DashboardAnalytics } from '../../admin-analytics.service';

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css']
})
export class AdminDashboardComponent implements OnInit {
  @ViewChild('orderChart') orderChartRef!: ElementRef<HTMLCanvasElement>;
  @ViewChild('revenueChart') revenueChartRef!: ElementRef<HTMLCanvasElement>;

  analytics: DashboardAnalytics | null = null;
  orderChart: Chart | null = null;
  revenueChart: Chart | null = null;

  constructor(private analyticsService: AdminAnalyticsService) {
    Chart.register(...registerables);
  }

  ngOnInit(): void {
    this.fetchDashboardAnalytics();
  }

  fetchDashboardAnalytics(): void {
    this.analyticsService.getDashboardAnalytics('week').subscribe({
      next: (data) => {
        this.analytics = data;
        this.renderCharts();
      },
      error: (err) => {
        console.error('Failed to fetch analytics', err);
      }
    });
  }

  renderCharts(): void {
    if (!this.analytics || !this.orderChartRef || !this.revenueChartRef) return;

    // Extract order trend data
    const orderTrend = this.analytics.orderTrend;
    const orderLabels = orderTrend.map(t => 
      new Date(t.date).toLocaleDateString('en-GB', {
        day: '2-digit',
        month: 'short',
        year: 'numeric',
      })
    );
    const orderData = orderTrend.map(t => t.count);

    // Extract revenue trend data
    const revenueTrend = this.analytics.revenueTrend;
    const revenueLabels = revenueTrend.map(t => 
      new Date(t.date).toLocaleDateString('en-GB', {
        day: '2-digit',
        month: 'short',
        year: 'numeric',
      })
    );
    const revenueData = revenueTrend.map(t => t.totalAmount);

    // Destroy previous charts to avoid overlap
    if (this.orderChart) {
      this.orderChart.destroy();
    }
    if (this.revenueChart) {
      this.revenueChart.destroy();
    }

    // Create the order trend chart
    this.orderChart = new Chart(this.orderChartRef.nativeElement, {
      type: 'line',
      data: {
        labels: orderLabels,
        datasets: [{
          label: 'Daily Orders',
          data: orderData,
          borderColor: 'rgb(75, 192, 192)',
          backgroundColor: 'rgba(75, 192, 192, 0.2)',
          fill: true,
          tension: 0.1,
        }]
      },
      options: {
        responsive: true,
        plugins: {
          legend: {
            display: true,
            position: 'top',
          }
        },
        scales: {
          x: {
            title: {
              display: true,
              text: 'Dates',
            }
          },
          y: {
            title: {
              display: true,
              text: 'Number of Orders',
            },
            beginAtZero: true,
          }
        }
      }
    });

    // Create the revenue trend chart
    this.revenueChart = new Chart(this.revenueChartRef.nativeElement, {
      type: 'bar',
      data: {
        labels: revenueLabels,
        datasets: [{
          label: 'Daily Revenue',
          data: revenueData,
          backgroundColor: 'rgba(255, 99, 132, 0.2)',
          borderColor: 'rgb(255, 99, 132)',
          borderWidth: 1,
        }]
      },
      options: {
        responsive: true,
        plugins: {
          legend: {
            display: true,
            position: 'top',
          }
        },
        scales: {
          x: {
            title: {
              display: true,
              text: 'Dates',
            }
          },
          y: {
            title: {
              display: true,
              text: 'Total Revenue',
            },
            beginAtZero: true,
          }
        }
      }
    });
  }
}
