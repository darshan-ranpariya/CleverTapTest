#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>

extern "C" {
    void _ShowSnackbar(const char* message) {
        NSString *msg = [NSString stringWithUTF8String:message];
        UIViewController *rootViewController = UnityGetGLViewController();
        UIView *view = rootViewController.view;
        
        UILabel *toastLabel = [[UILabel alloc] init];
        toastLabel.text = msg;
        toastLabel.font = [UIFont systemFontOfSize:14.0];
        toastLabel.textColor = [UIColor whiteColor];
        toastLabel.backgroundColor = [[UIColor blackColor] colorWithAlphaComponent:0.8];
        toastLabel.textAlignment = NSTextAlignmentCenter;
        toastLabel.layer.cornerRadius = 10;
        toastLabel.layer.masksToBounds = YES;
        toastLabel.numberOfLines = 0; 
        
        CGSize maxSize = CGSizeMake(view.frame.size.width - 40, view.frame.size.height - 100);
        CGSize expectedSize = [toastLabel sizeThatFits:maxSize];
        
        CGFloat padding = 20;
        CGFloat width = expectedSize.width + padding;
        CGFloat height = expectedSize.height + padding;
        CGFloat x = (view.frame.size.width - width) / 2;
        CGFloat y = view.frame.size.height - 100;
        
        toastLabel.frame = CGRectMake(x, y, width, height);
        [view addSubview:toastLabel];
        
        toastLabel.alpha = 0.0;
        [UIView animateWithDuration:0.5 animations:^{
            toastLabel.alpha = 1.0;
        } completion:^(BOOL finished) {
            [UIView animateWithDuration:0.5 delay:2.0 options:UIViewAnimationOptionCurveEaseOut animations:^{
                toastLabel.alpha = 0.0;
            } completion:^(BOOL finished) {
                [toastLabel removeFromSuperview];
            }];
        }];
    }
}