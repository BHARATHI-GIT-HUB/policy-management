import { Component, Input, OnInit } from '@angular/core';
import Chart from 'chart.js/auto';
import { ChartData, ChartDataset } from 'chart.js/auto';

@Component({
  selector: 'app-chart',
  templateUrl: './chart.component.html',
  styleUrls: ['./chart.component.scss'],
})
export class ChartComponent implements OnInit {
  @Input()
  listofProvider: string[] = [];
  @Input()
  totalPlanCount: number[] = [];
  @Input()
  doughnutData: number[] = [];

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
        labels: this.listofProvider,
        datasets: [
          {
            label: 'Plans',
            data: this.totalPlanCount,
            borderWidth: 1,
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

            ticks: {
              // forces step size to be 50 units
              stepSize: 1,
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
        labels: ['Plans', 'Provider', 'Enrolled'],
        datasets: [
          {
            label: 'My First Dataset',
            data: this.doughnutData,
            backgroundColor: [
              'rgb(255, 99, 132)',
              'rgb(54, 162, 235)',
              'rgb(255, 205, 86)',
            ],
            hoverOffset: 4,
          },
        ],
      },
      options: {
        plugins: {
          title: {
            display: true,
            text: 'Enrollment Ratio',
          },
        },
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
