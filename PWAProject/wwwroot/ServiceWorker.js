const casheName = "Pwa Demo Project";
const assets = [
    "/"
];

self.addEventListener("install", installEvent => {
    console.log("test3");
    installEvent.waitUntil(
        caches.open(casheName).then(cache => {
            cache.addAll(assets)
        })
         
    )
});

// activate event
self.addEventListener('activate', evt => {
    evt.waitUntil(
        caches.keys().then(keys => {
            return Promise.all(keys
                .filter(key => key !== assets)
                .map(key => caches.delete(key))
            );
        })
    );
});

self.addEventListener("fetch", fetchEvent => {
    fetchEvent.respondWith(
        caches.match(fetchEvent.request).then(res => {
            return res || fetch(fetchEvent.request)
        })
    )
});