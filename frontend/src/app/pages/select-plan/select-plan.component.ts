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
  minValue: number = 0; // Min value
  maxValue: number = 0; // Max value
  stepValue: number = 1; // Step value
  sliderValue: number = 5; // Default value
  premium: number = 0;
  planData: any;
  // @Output() sliderValueChange = new EventEmitter<number>(); // Output event to emit slider value

  sumInsured: number[] = [];
  handlePosition: number = 50; // Initial position of the handle
  isLoading: boolean = true;

  constructor(
    private planService: PlanService,
    private route: ActivatedRoute,
    private el: ElementRef
  ) {}

  ngOnInit(): void {
    this.calculatePremium();
    this.route.params.subscribe((params) => {
      // Extracting id parameter
      this.id = <number>params['id'];
    });
    this.getPlanById(this.id);
    if (this.planData != null && this.planData != undefined) {
      this.isLoading = true;
    }
  }

  getPlanById(id: number) {
    this.planService.getById(id).subscribe((data) => {
      console.log(data);
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
    console.log(this.sliderValue);
  }
  startDrag(event: MouseEvent | TouchEvent) {
    event.preventDefault();
    document.addEventListener('drag', this.onDrag.bind(this));
    document.addEventListener('dragend', this.stopDrag.bind(this));
  }

  onDrag(event: MouseEvent | TouchEvent) {
    const slider = document.getElementById('slider');
    if (!slider) return;

    const sliderRect = slider.getBoundingClientRect();
    const newPosition =
      ((event instanceof MouseEvent
        ? event.clientX
        : event.touches[0].clientX - sliderRect.left) /
        sliderRect.width) *
      100;
    this.handlePosition = Math.max(0, Math.min(100, newPosition)); // Ensure position stays within bounds
  }

  stopDrag() {
    document.removeEventListener('drag', this.onDrag.bind(this));
    document.removeEventListener('dragend', this.stopDrag.bind(this));
  }
}
