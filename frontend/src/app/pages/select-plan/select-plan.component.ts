import { Component, EventEmitter, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-select-plan',
  templateUrl: './select-plan.component.html',
  styleUrl: './select-plan.component.scss',
})
export class SelectPlanComponent implements OnInit {
  step: number = 2;
  minValue: number = 0; // Min value
  maxValue: number = 100; // Max value
  stepValue: number = 1; // Step value
  sliderValue: number = 5; // Default value
  premium: number = 0;
  // @Output() sliderValueChange = new EventEmitter<number>(); // Output event to emit slider value

  sumInsured: number[] = [1, 2, 3, 4, 5, 6, 7, 8, 9];
  handlePosition: number = 50; // Initial position of the handle

  ngOnInit(): void {
    this.calculatePremium();
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
