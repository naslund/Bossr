import { BossrAppPage } from './app.po';

describe('bossr-app App', () => {
  let page: BossrAppPage;

  beforeEach(() => {
    page = new BossrAppPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
