import { NgModule } from '@angular/core';
import { NZ_ICONS, NzIconModule } from 'ng-zorro-antd/icon';

import {
  MenuFoldOutline,
  MenuUnfoldOutline,
  FormOutline,
  DashboardOutline,
  FileDoneOutline,
  UserOutline,
  PieChartOutline,
  AppstoreAddOutline,
  SettingOutline,
  UserAddOutline,
} from '@ant-design/icons-angular/icons';

const icons = [
  MenuFoldOutline,
  MenuUnfoldOutline,
  DashboardOutline,
  FormOutline,
  FileDoneOutline,
  UserOutline,
  PieChartOutline,
  SettingOutline,
  AppstoreAddOutline,
  UserAddOutline,
];

@NgModule({
  imports: [NzIconModule],
  exports: [NzIconModule],
  providers: [{ provide: NZ_ICONS, useValue: icons }],
})
export class IconsProviderModule {}
