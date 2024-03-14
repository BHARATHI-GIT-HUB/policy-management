import {
  Component,
  ElementRef,
  EventEmitter,
  HostListener,
  OnInit,
  Output,
} from '@angular/core';
import { PlanService } from '../../services/plan.service';
import { CloudFilled } from '@ant-design/icons';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-select-plan',
  templateUrl: './select-plan.component.html',
  styleUrl: './select-plan.component.scss',
})
export class SelectPlanComponent implements OnInit {
  id: number = 0;
  step: number = 2;
  minValue: number = 0;
  maxValue: number = 0;
  stepValue: number = 1;
  sliderValue: number = 5;
  premium: number = 0;
  planData: any;

  sumInsured: number[] = [];
  handlePosition: number = 50;
  isLoading: boolean = true;

  constructor(
    private planService: PlanService,
    private route: ActivatedRoute,
    private el: ElementRef
  ) {}

  ngOnInit(): void {
    this.calculatePremium();
    this.route.params.subscribe((params) => {
      this.id = <number>params['id'];
    });
    if (this.id > 0) this.getPlanById(this.id);
    if (this.planData != null && this.planData != undefined) {
      this.isLoading = true;
    }
  }

  getPlanById(id: number) {
    this.planService.getById(id).subscribe((data) => {
      this.planData = data;
      this.maxValue = data.maxCoverageAmount / 100000;
      for (let index = 0; index <= this.maxValue; index++) {
        this.sumInsured.push(index);
      }
    });
  }
  calculatePremium() {
    const premiumRate = 0.08;
    const selectedCoverage = this.sliderValue * 100000;
    const timePeroid = 5;
    this.premium = selectedCoverage / (timePeroid * 1);
  }

  onSliderChange(event: Event) {
    const inputElement = event.target as HTMLInputElement;
    this.sliderValue = parseInt(inputElement.value);
    this.calculatePremium();
  }
}
