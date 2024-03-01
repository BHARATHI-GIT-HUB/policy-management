import { Component, OnInit } from '@angular/core';
import Chart from 'chart.js/auto';
import { ChartData, ChartDataset } from 'chart.js/auto';

@Component({
  selector: 'app-chart',
  templateUrl: './chart.component.html',
  styleUrls: ['./chart.component.scss'],
})
export class ChartComponent implements OnInit {
  title = 'ng-chart';

  barchart: any = [];
  doughnut: any = [];
  linechart: any = [];
  DATA_COUNT = 12;
  labels: string[] = [];
  months = [
    'January',
    'February',
    'March',
    'April',
    'May',
    'June',
    'July',
    'August',
    'September',
    'October',
    'November',
    'December',
  ];

  datapoints: any = [0, 20, 20, 60, 60, 120, NaN, 180, 120, 125, 105, 110, 170];

  constructor() {}

  linechartdata: ChartData<'line', ChartDataset[], string> = {
    labels: this.months,
    datasets: [
      {
        label: 'ICIC',
        data: this.datapoints,
        fill: false,
        tension: 0.4,
      },
      {
        label: 'Star',
        data: this.datapoints,
        fill: false,
        tension: 0.4,
      },
      {
        label: 'HDFC',
        data: this.datapoints,
        fill: false,
        tension: 0, // Adjust as necessary
      },
    ],
  };

  ngOnInit() {
    for (let i = 0; i < this.DATA_COUNT; ++i) {
      this.labels.push(i.toString());
    }
    this.BarChart();
    this.DoughnutChart();
    this.LineChart();
  }

  BarChart() {
    this.barchart = new Chart('barchart', {
      type: 'bar',
      data: {
        labels: ['HDFC', 'ICIC', 'LIC', 'STAR', 'CANADA'],
        datasets: [
          {
            label: 'plans',
            data: [12, 19, 3, 5, 2, 3],
            borderWidth: 0.5,
          },
        ],
      },
      options: {
        scales: {
          x: {
            grid: {
              display: false,
            },
          },
          y: {
            beginAtZero: true,
            grid: {
              display: false,
            },
          },
        },
      },
    });
  }

  DoughnutChart() {
    this.doughnut = new Chart('doughnut', {
      type: 'doughnut',
      data: {
        labels: ['Provider', 'Plans', 'Enrolled'],
        datasets: [
          {
            label: 'My First Dataset',
            data: [10, 50, 30],
            backgroundColor: [
              'rgb(255, 99, 132)',
              'rgb(54, 162, 235)',
              'rgb(255, 205, 86)',
            ],
            hoverOffset: 4,
          },
        ],
      },
    });
  }

  LineChart() {
    this.linechart = new Chart('linechart', {
      type: 'line',
      data: this.linechartdata,
      options: {
        responsive: true,
        plugins: {
          title: {
            display: true,
            text: 'Enrollments ',
          },
        },
        interaction: {
          intersect: false,
        },
        scales: {
          x: {
            display: true,
            beginAtZero: true,

            title: {
              display: true,
            },
          },
          y: {
            display: true,
            beginAtZero: true,

            title: {
              display: true,
              text: 'Activated',
            },
            suggestedMin: 0,
            suggestedMax: 200,
            ticks: {
              // forces step size to be 50 units
              stepSize: 10,
            },
          },
        },
      },
    });

    // Logging to check if linechart initialization is successful
  }
}
